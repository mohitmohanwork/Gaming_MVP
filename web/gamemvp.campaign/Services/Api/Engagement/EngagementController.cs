using Blaash.Gaming.Service.Common.Constants;
using gamemvp.campaign.Consts;
using gamemvp.campaign.Services.Api.Campaign.Models;
using gamemvp.common.Services;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using ZNxt.Net.Core.Consts;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Interfaces;
using ZNxt.Net.Core.Model;

namespace gamemvp.campaign.Services.Api.Campaign
{
    public  class EngagementController : MvpBaseController
    {

        public EngagementController(IHttpContextProxy httpContextProxy,
            IDBService dBService,
            IRDBService rDBService,
            ILogger logger, IResponseBuilder responseBuilder)
      : base(httpContextProxy, dBService, rDBService, logger, responseBuilder)
        {

        }

        #region Campaign 
        [Route(CampaignConsts.SERVICE_API_PREFIX + "/addengagement", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject AddCampaign()
        {
            var request = GetRequestBody<EngagementDbo>();
            var results = new Dictionary<string, string>();
            _logger.Debug("Validation model");
            if (request.IsValidModel(out results))
            {
                if (_rDBService.GetCount(GameMvpCommonConsts.Collections.ENGAGEMENT, new JObject() { [nameof(request.short_name)] = request.short_name, [nameof(request.tenant_id)] = request.tenant_id }) == 0)
                {
                   
                        SetUser(request);
                        var id = _rDBService.WriteData<EngagementDbo>(request);
                        request.engagement_id = id;
                        return _responseBuilder.Success(request.ToJObject());
                }
                else
                {
                    JObject errors = new JObject();
                    errors["key"] = $"Duplicate addengagement name {request.short_name},  tenant_id: {request.tenant_id}";
                    return _responseBuilder.BadRequest(errors);
                }
            }
            else
            {
                return ModelValidationFailResponse(results);
            }
        }
        [Route(CampaignConsts.SERVICE_API_PREFIX + "/updateengagementbyid", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject UpdateCampaign()
        {
            var request = GetRequestBody<EngagementDbo>();
            var results = new Dictionary<string, string>();
            _logger.Debug("Validation model");
            if (request.IsValidModel(out results))
            {
                if (_rDBService.GetCount(GameMvpCommonConsts.Collections.ENGAGEMENT, new JObject() { [nameof(request.engagement_id)] = request.engagement_id}) != 0)
                {

                    SetUser(request);
                    var result = _rDBService.Update<EngagementDbo>(request);
                    if (result)
                    {
                        return _responseBuilder.Success(request.ToJObject());
                    }
                    else
                    {
                        return _responseBuilder.ServerError();
                    }
                }
                else
                {
                    JObject errors = new JObject();
                    errors["key"] = $"Engagement not found,  id: {request.engagement_id}";
                    return _responseBuilder.NotFound(errors,request.ToJObject());
                }
            }
            else
            {
                return ModelValidationFailResponse(results);
            }
        }
        [Route(CampaignConsts.SERVICE_API_PREFIX + "/updatestatus", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject UpdateCampaignStatus()
        {
            var request = GetRequestBody<EngagementDbo>();
            var emg = _rDBService.Get<EngagementDbo>(GameMvpCommonConsts.Collections.ENGAGEMENT, 1, 0, new JObject() { [nameof(request.engagement_id)] = request.engagement_id });
            if (emg.Any())
            {
                emg.First().engagement_status_id = request.engagement_status_id;
                SetUser(request);
                var result = _rDBService.Update<EngagementDbo>(emg.First());
                if (result)
                {
                    return _responseBuilder.Success(emg.First().ToJObject());
                }
                else
                {
                    return _responseBuilder.ServerError();
                }
            }
            else
            {
                JObject errors = new JObject();
                errors["key"] = $"Engagement not found,  id: {request.engagement_id}";
                return _responseBuilder.NotFound(errors, request.ToJObject());
            }

        }



        [Route(CampaignConsts.SERVICE_API_PREFIX + "/engagementbyid", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GetCampaignById()
        {
            var campaignid = _httpContextProxy.GetQueryString("engagement_id");
            long ingameid = 0;
            if (!string.IsNullOrEmpty(campaignid) && long.TryParse(campaignid, out ingameid))
            {
                var journey = _rDBService.Get<EngagementDbo>(GameMvpCommonConsts.Collections.ENGAGEMENT,1,0,new JObject() { ["engagement_id"] = campaignid });
                if (journey.Any())
                {
                    return _responseBuilder.Success(journey.First().ToJObject());
                }
                else
                {
                    JObject errors = new JObject();
                    errors["key"] = $"engagemen not found, id {campaignid}";
                    return _responseBuilder.NotFound(errors);
                }
            }
            else
            {
                JObject errors = new JObject();
                errors["key"] = $"Bad request invalid engagemen id {campaignid}";
                return _responseBuilder.BadRequest(errors);
            }
        }
        [Route(CampaignConsts.SERVICE_API_PREFIX + "/engagementbyfilters", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GetCampaignByFilter()
        {
            var filters = GetFiltersFromQueryString();
            var pagedata = GetRequestPaggedData();
            var game = _rDBService.Get<EngagementDbo>(GameMvpCommonConsts.Collections.ENGAGEMENT, pagedata.PageSize, pagedata.Skipped, filters);
            return _responseBuilder.SuccessPaggedData(game.ToList().ToJArray(), pagedata.CurrentPage, pagedata.PageSize);

        }

        #endregion 


    }
}
