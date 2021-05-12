using gamemvp.common.Consts;
using gamemvp.common.Services;
using gamemvp.tenantmgmt.Consts;
using gamemvp.tenantmgmt.Services.Api.Tenant.Modules;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using ZNxt.Net.Core.Consts;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Interfaces;
using ZNxt.Net.Core.Model;
namespace gamemvp.tenantmgmt.Api
{
    public class TenantController : MvpBaseController
    {
        
        public TenantController(IHttpContextProxy httpContextProxy,
            IDBService dBService,
            IRDBService rDBService,
            ILogger logger,IResponseBuilder responseBuilder)
      : base(httpContextProxy, dBService, rDBService, logger, responseBuilder)
        {
            
        }

        [Route(TenantMgmtConsts.SERVICE_API_PREFIX + "/addtenant", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject AddTenant()
        {
            var request = GetRequestBody<Services.Api.Tenant.Modules.TenantDbo>();
            var results = new Dictionary<string, string>();
            _logger.Debug("Validation model");
            if (request.IsValidModel(out results))
            {
                if (_rDBService.GetCount(GameMvpCommonConsts.Collections.TENANT, new JObject() { ["name"] = request.name }) == 0)
                {
                    if (_rDBService.GetCount(GameMvpCommonConsts.Collections.TENANT, new JObject() { ["email"] = request.email }) == 0)
                    {
                        SetUser(request);
                        var id = _rDBService.WriteData<TenantDbo>(request);
                        request.tenant_id = id;
                        return _responseBuilder.Success(request.ToJObject());
                    }
                    else
                    {
                        JObject errors = new JObject();
                        errors["key"] = $"Duplicate email {request.email}";
                        return _responseBuilder.BadRequest(errors);
                    }
                }
                else
                {
                    JObject errors = new JObject();
                    errors["key"] = $"Duplicate name {request.name}";
                    return _responseBuilder.BadRequest(errors);
                }
            }
            else
            {
                return ModelValidationFailResponse(results);
            }
        }
        [Route(TenantMgmtConsts.SERVICE_API_PREFIX + "/addtenantclient", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject AddTenantClient()
        {
            var request = _httpContextProxy.GetRequestBody<Services.Api.Tenant.Modules.TenantClientDbo>();
            var results = new Dictionary<string, string>();
            _logger.Debug("Validation model");
            if (request.IsValidModel(out results))
            {
                var jsonrequest = request.ToJObject();
                jsonrequest.Remove(nameof(request.created_by));
                jsonrequest.Remove(nameof(request.created_on));
                jsonrequest.Remove(nameof(request.updated_by));
                jsonrequest.Remove(nameof(request.updated_on));
                jsonrequest.Remove(nameof(request.id));

                var exitingdata = _rDBService.Get<TenantClientDbo>(GameMvpCommonConsts.Collections.TENANT_CLIENT,1,0, jsonrequest);
                if (!exitingdata.Any())
                {
                    jsonrequest[CommonConst.CommonField.DISPLAY_ID] = CommonUtility.GetNewID();
                    var data = _rDBService.WriteData<TenantClientDbo>(request);
                    request.id = data;
                    return _responseBuilder.Success(request.ToJObject());
                }
                else
                {
                    return _responseBuilder.Success(exitingdata.First().ToJObject());
                }
            }
            else
            {
                return ModelValidationFailResponse(results);
            }
        }
        [Route(TenantMgmtConsts.SERVICE_API_PREFIX + "/clienttenant", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL + "," + CommonConst.CommonField.API_AUTH_TOKEN)]
        public JObject GetTenantClient()
        {
            var clientid = _httpContextProxy.GetQueryString("client_id");
            var results = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(clientid))
            {
                var filter = JObject.Parse("{'client_id':'" + clientid + "', 'is_active': true}");
                var data = _rDBService.Get<TenantClientDbo>(GameMvpCommonConsts.Collections.TENANT_CLIENT,1,0, filter);
                if (data.Any())
                {
                    return _responseBuilder.Success(data.First().ToJObject());
                }
                else
                {
                    return _responseBuilder.NotFound($"client_id {clientid } not found");
                }
            }
            else
            {
                results["client_id"] = "client_id query parameter is missing";
                return ModelValidationFailResponse(results);
            }
        }
     
    }
}
