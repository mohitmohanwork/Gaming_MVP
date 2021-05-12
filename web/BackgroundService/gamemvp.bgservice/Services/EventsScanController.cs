using gamemvp.bgservice.Consts;
using gamemvp.common.Consts;
using gamemvp.common.Models.Event;
using gamemvp.common.Models.Identity;
using gamemvp.common.Models.ResponseCode;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using ZNxt.Net.Core.Consts;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Interfaces;
using ZNxt.Net.Core.Model;

namespace gamemvp.bgservice.Services
{
    public class EventsScanController
    {
        private IDBService _dBService;
        private readonly ILogger _logger;
        private readonly IApiGatewayService _apiGatewayService;
        private readonly IInMemoryCacheService _inMemoryCacheService;
        private readonly string SSOBaseUrl = "";
        private readonly string TenantServiceBaseUrl = "";
        private readonly string CreateUserEndPoint = "/idty/createuser";
        private readonly string GetTenantEndPoint = "/tenm/clienttenant";

        public EventsScanController(IDBService dBService,
            ILogger logger,IApiGatewayService apiGatewayService,IInMemoryCacheService inMemoryCacheService)
        {
            _dBService = dBService;
            _logger = logger;
            _apiGatewayService = apiGatewayService;
            _inMemoryCacheService = inMemoryCacheService;
            SSOBaseUrl = CommonUtility.GetAppConfigValue("SSOUrl");
            TenantServiceBaseUrl = CommonUtility.GetAppConfigValue("TenantServiceBaseUrl");
        }

        public void ProcessEvent()
        {
            var currenttimestamp = CommonUtility.GetTimestamp(DateTime.UtcNow);
            ProcessLoginEvent(currenttimestamp);
        }

        private void ProcessLoginEvent(string currenttimestamp)
        {
            JObject filter = new JObject();
            filter[GameMvpCommonConsts.CommonKeys.EVENT_NAME] = GameMvpCommonConsts.EventTypes.PLAYER_LOGIN;
            filter[GameMvpCommonConsts.CommonKeys.IS_PROCESSED] = false;
            foreach (var item in _dBService.Get(GameMvpCommonConsts.Collections.EVENT, new RawQuery(filter.ToString())))
            {
                try
                {
                    var eventdata = Newtonsoft.Json.JsonConvert.DeserializeObject<EventTrackModel>(item.ToString());
                    var userdata = new CreateUserModel();
                    var tenant = GetTenant(eventdata.client_id);
                    userdata.tenant_id = long.Parse(tenant["tenant_id"].ToString());
                    userdata.user_name = $"{userdata.tenant_id}_{eventdata.properties.First(f => f.key == GameMvpCommonConsts.CommonKeys.USER_ID).value}";
                    userdata.user_id = long.Parse($"{userdata.tenant_id}{CommonUtility.GetUnixTimestamp(DateTime.UtcNow)}").ToString();
                    userdata.first_name = eventdata.properties.First(f => f.key == GameMvpCommonConsts.CommonKeys.FIRST_NAME).value;
                    userdata.last_name = eventdata.properties.First(f => f.key == GameMvpCommonConsts.CommonKeys.LAST_NAME).value;
                    userdata.middle_name = eventdata.properties.FirstOrDefault(f => f.key == GameMvpCommonConsts.CommonKeys.MIDDLE_NAME)?.value;
                    userdata.mobile_number = eventdata.properties.First(f => f.key == CommonConst.CommonField.PHONE).value;
                    userdata.email = eventdata.properties.First(f => f.key == CommonConst.CommonField.EMAIL).value;
                    var response = _apiGatewayService.CallAsync(CommonConst.ActionMethods.POST, CreateUserEndPoint, "", userdata.ToJObject(), null, SSOBaseUrl).GetAwaiter().GetResult();
                    if (response[CommonConst.CommonField.HTTP_RESPONE_CODE].ToString() == CommonConst._1_SUCCESS.ToString() || response[CommonConst.CommonField.HTTP_RESPONE_CODE].ToString() == MvpApiKeyResponseCode._ALREADY_EXISTS.ToString())
                    {
                        eventdata.is_processed = true;
                        filter = new JObject();
                        filter[CommonConst.CommonField.DISPLAY_ID] = eventdata.id;
                        _dBService.Update(GameMvpCommonConsts.Collections.EVENT, new RawQuery(filter.ToString()), eventdata.ToJObject(), false);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, ex);
                }

            }


        }

        private  JObject GetTenant(long clientId)
        {
            var tenant = _inMemoryCacheService.Get<JObject>($"client_{clientId}");
            if (tenant == null)
            {
                tenant = FetchTenant(clientId);
            }
            if(tenant == null)
            {
                throw new Exception($"Tenant not found for client {clientId}");
            }
            return tenant;

        }

        private JObject FetchTenant(long clientId)
        {
            var response = _apiGatewayService.CallAsync(CommonConst.ActionMethods.GET, GetTenantEndPoint, $"client_id={clientId}",null, null, TenantServiceBaseUrl).GetAwaiter().GetResult();
            if (response[CommonConst.CommonField.HTTP_RESPONE_CODE].ToString() == CommonConst._1_SUCCESS.ToString() && response[CommonConst.CommonField.HTTP_RESPONE_DATA]!=null)
            {
                _inMemoryCacheService.Put<JObject>($"client_{clientId}", response[CommonConst.CommonField.HTTP_RESPONE_DATA] as JObject);
                return response[CommonConst.CommonField.HTTP_RESPONE_DATA] as JObject;
            }
            return null;
        }
    }
}
