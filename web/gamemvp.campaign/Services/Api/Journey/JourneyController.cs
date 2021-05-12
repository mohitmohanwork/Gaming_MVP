using System.Collections.Generic;
using System.Linq;
using Blaash.Gaming.Service.Common.Constants;
using gamemvp.campaign.Consts;
using gamemvp.campaign.Services.Api.Journey.Models;
using gamemvp.common.Services;
using Newtonsoft.Json.Linq;
using ZNxt.Net.Core.Consts;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Interfaces;
using ZNxt.Net.Core.Model;

namespace gamemvp.campaign.Services.Api.Journey
{
    public  class JourneyController : MvpBaseController
    {

        public JourneyController(IHttpContextProxy httpContextProxy,
            IDBService dBService,
            IRDBService rDBService,
            ILogger logger, IResponseBuilder responseBuilder)
      : base(httpContextProxy, dBService, rDBService, logger, responseBuilder)
        {

        }

        #region journey 
        [Route(CampaignConsts.SERVICE_API_PREFIX + "/addjourney", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject AddJourney()
        {
            var request = GetRequestBody<JourneyDbo>();
            return AddJourney(request);
        }
        [Route(CampaignConsts.SERVICE_API_PREFIX + "/addupdatejourney", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject AddUpdateJourney()
        {
            var request = GetRequestBody<JourneyDbo>();
            if (request.journey_id == 0)
            {
                return AddJourney(request);
            }
            else
            {
                return UpdateJourney(request);
            }
        }

        private JObject AddJourney(JourneyDbo request)
        {
            var results = new Dictionary<string, string>();
            _logger.Debug("Validation model");
            if (request.IsValidModel(out results))
            {
                if (_rDBService.GetCount(GameMvpCommonConsts.Collections.JOURNEY, new JObject() { [nameof(request.name)] = request.name, [nameof(request.tenant_id)] = request.tenant_id }) == 0)
                {

                    SetUser(request);
                    var id = _rDBService.WriteData<JourneyDbo>(request);
                    request.journey_id = id;
                    return _responseBuilder.Success(request.ToJObject());
                }
                else
                {
                    JObject errors = new JObject();
                    errors["key"] = $"Duplicate journey name {request.name},  tenant_id: {request.tenant_id}";
                    return _responseBuilder.BadRequest(errors);
                }
            }
            else
            {
                return ModelValidationFailResponse(results);
            }
        }

        [Route(CampaignConsts.SERVICE_API_PREFIX + "/updatejourney", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject UpdateJourney()
        {
            var request = GetRequestBody<JourneyDbo>();
            return UpdateJourney(request);
        }

        private JObject UpdateJourney(JourneyDbo request)
        {
            var results = new Dictionary<string, string>();
            _logger.Debug("Validation model");
            if (request.IsValidModel(out results))
            {
                if (_rDBService.GetCount(GameMvpCommonConsts.Collections.JOURNEY, new JObject() { [nameof(request.journey_id)] = request.journey_id }) != 0)
                {

                    SetUser(request);
                    var result = _rDBService.Update<JourneyDbo>(request);
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
                    errors["key"] = $"journey not found,  id: {request.journey_id}";
                    return _responseBuilder.NotFound(errors, request.ToJObject());
                }
            }
            else
            {
                return ModelValidationFailResponse(results);
            }
        }

        [Route(CampaignConsts.SERVICE_API_PREFIX + "/journeybyid", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GetJourneyById()
        {
            var journeyid = _httpContextProxy.GetQueryString("journey_id");
            long ingameid = 0;
            if (!string.IsNullOrEmpty(journeyid) && long.TryParse(journeyid, out ingameid))
            {
                var journey = _rDBService.Get<JourneyDbo>(GameMvpCommonConsts.Collections.JOURNEY,1,0,new JObject() { ["journey_id"] = journeyid });
                if (journey.Any())
                {
                    return _responseBuilder.Success(journey.First().ToJObject());
                }
                else
                {
                    JObject errors = new JObject();
                    errors["key"] = $"journey not found, journey id {journeyid}";
                    return _responseBuilder.NotFound(errors);
                }
            }
            else
            {
                JObject errors = new JObject();
                errors["key"] = $"Bad request invalid journey id {journeyid}";
                return _responseBuilder.BadRequest(errors);
            }
        }
        [Route(CampaignConsts.SERVICE_API_PREFIX + "/journeybyfilters", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GetJourneyByFilter()
        {

            var filters = GetFiltersFromQueryString();
            var pagedata = GetRequestPaggedData();
            var game = _rDBService.Get<JourneyDbo>(GameMvpCommonConsts.Collections.JOURNEY, pagedata.PageSize, pagedata.Skipped, filters);

            return _responseBuilder.SuccessPaggedData(game.ToList().ToJArray(), pagedata.CurrentPage, pagedata.PageSize);

        }


        #endregion journey 

        #region engagement_journey
        [Route(CampaignConsts.SERVICE_API_PREFIX + "/addengagementjourney", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject AddEngagementJourney()
        {


            var request = GetRequestBody<JourneyEngagementDbo>();
            var results = new Dictionary<string, string>();
            _logger.Debug("Validation model");
            if (request.IsValidModel(out results))
            {
                if (_rDBService.GetCount(GameMvpCommonConsts.Collections.ENGAGEMENT_JOURNEY, new JObject() { [nameof(request.name)] = request.name, [nameof(request.journey_id)] = request.journey_id }) == 0)
                {

                    SetUser(request);
                    var id = _rDBService.WriteData<JourneyEngagementDbo>(request);
                    request.engagement_journey_id = id;
                    return _responseBuilder.Success(request.ToJObject());
                }
                else
                {
                    JObject errors = new JObject();
                    errors["key"] = $"Duplicate engagement_journey_id  {request.name},  journey_id: {request.journey_id}";
                    return _responseBuilder.BadRequest(errors);
                }
            }
            else
            {
                return ModelValidationFailResponse(results);
            }
        }

        [Route(CampaignConsts.SERVICE_API_PREFIX + "/updateengagementjourney", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject UpdateEngagementJourney()
        {

            var request = GetRequestBody<JourneyEngagementDbo>();
            var results = new Dictionary<string, string>();
            _logger.Debug("Validation model");
            if (request.IsValidModel(out results))
            {
                if (_rDBService.GetCount(GameMvpCommonConsts.Collections.ENGAGEMENT_JOURNEY, new JObject() { [nameof(request.engagement_journey_id)] = request.engagement_journey_id}) != 0)
                {

                    SetUser(request);
                    var result = _rDBService.Update<JourneyEngagementDbo>(request);
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
                    errors["key"] = $"journey engagement_journey_id not found, id: {request.engagement_journey_id}";
                    return _responseBuilder.NotFound(errors, request.ToJObject());
                }
            }
            else
            {
                return ModelValidationFailResponse(results);
            }
        }

        [Route(CampaignConsts.SERVICE_API_PREFIX + "/engagementjourneybyid", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GetEngagementJourneyById()
        {

            var engagementjourneyid = _httpContextProxy.GetQueryString("engagement_journey_id");
            long ingameid = 0;
            if (!string.IsNullOrEmpty(engagementjourneyid) && long.TryParse(engagementjourneyid, out ingameid))
            {
                var journey = _rDBService.Get<JourneyEngagementDbo>(GameMvpCommonConsts.Collections.ENGAGEMENT_JOURNEY, 1, 0, new JObject() { ["engagement_journey_id"] = engagementjourneyid });
                if (journey.Any())
                {
                    return _responseBuilder.Success(journey.First().ToJObject());
                }
                else
                {
                    JObject errors = new JObject();
                    errors["key"] = $"engagement_journey not found, id {engagementjourneyid}";
                    return _responseBuilder.NotFound(errors);
                }
            }
            else
            {
                JObject errors = new JObject();
                errors["key"] = $"Bad request invalid engagement_journey {engagementjourneyid}";
                return _responseBuilder.BadRequest(errors);
            }
        }

        [Route(CampaignConsts.SERVICE_API_PREFIX + "/engagementjourneybyfilter", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GetEngagementJourneyByFilter()
        {
            var filters = GetFiltersFromQueryString();
            var pagedata = GetRequestPaggedData();
            var game = _rDBService.Get<JourneyEngagementDbo>(GameMvpCommonConsts.Collections.ENGAGEMENT_JOURNEY, pagedata.PageSize, pagedata.Skipped, filters);
            return _responseBuilder.SuccessPaggedData(game.ToList().ToJArray(), pagedata.CurrentPage, pagedata.PageSize);

        }


        #endregion journeydetails

        #region journeydetails
        [Route(CampaignConsts.SERVICE_API_PREFIX + "/addjourneydetails", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject AddJourneyDetails()
        {


            var request = GetRequestBody<JourneyDetailsDbo>();
            var results = new Dictionary<string, string>();
            _logger.Debug("Validation model");
            if (request.IsValidModel(out results))
            {
                if (_rDBService.GetCount(GameMvpCommonConsts.Collections.JOURNEY_DETAILS, new JObject() { [nameof(request.name)] = request.name, [nameof(request.journey_id)] = request.journey_id }) == 0)
                {

                    SetUser(request);
                    var id = _rDBService.WriteData<JourneyDetailsDbo>(request);
                    request.journey_detail_id = id;
                    return _responseBuilder.Success(request.ToJObject());
                }
                else
                {
                    JObject errors = new JObject();
                    errors["key"] = $"Duplicate journey details name {request.name},  journey_id: {request.journey_id}";
                    return _responseBuilder.BadRequest(errors);
                }
            }
            else
            {
                return ModelValidationFailResponse(results);
            }
        }
        [Route(CampaignConsts.SERVICE_API_PREFIX + "/updatejourneydetails", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject UpdateJourneyDetails()
        {

            var request = GetRequestBody<JourneyDetailsDbo>();
            var results = new Dictionary<string, string>();
            _logger.Debug("Validation model");
            if (request.IsValidModel(out results))
            {
                if (_rDBService.GetCount(GameMvpCommonConsts.Collections.JOURNEY_DETAILS, new JObject() { [nameof(request.journey_id)] = request.journey_id }) != 0)
                {

                    SetUser(request);
                    var result = _rDBService.Update<JourneyDetailsDbo>(request);
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
                    errors["key"] = $"journey details not found, journey details  id: {request.journey_detail_id}";
                    return _responseBuilder.NotFound(errors, request.ToJObject());
                }
            }
            else
            {
                return ModelValidationFailResponse(results);
            }
        }
        [Route(CampaignConsts.SERVICE_API_PREFIX + "/journeydetailsbyid", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GetJourneyDetailsById()
        {

            var journeydetailid = _httpContextProxy.GetQueryString("journey_detail_id");
            long ingameid = 0;
            if (!string.IsNullOrEmpty(journeydetailid) && long.TryParse(journeydetailid, out ingameid))
            {
                var journey = _rDBService.Get<JourneyDetailsDbo>(GameMvpCommonConsts.Collections.JOURNEY_DETAILS, 1, 0, new JObject() { ["journey_detail_id"] = journeydetailid });
                if (journey.Any())
                {
                    return _responseBuilder.Success(journey.First().ToJObject());
                }
                else
                {
                    JObject errors = new JObject();
                    errors["key"] = $"journey detail not found, id {journeydetailid}";
                    return _responseBuilder.NotFound(errors);
                }
            }
            else
            {
                JObject errors = new JObject();
                errors["key"] = $"Bad request invalid journey detail {journeydetailid}";
                return _responseBuilder.BadRequest(errors);
            }
        }
        [Route(CampaignConsts.SERVICE_API_PREFIX + "/journeydetailsbyfilter", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GetJourneydetailsByFilter()
        {
            var filters = GetFiltersFromQueryString();
            var pagedata = GetRequestPaggedData();
            var game = _rDBService.Get<JourneyDetailsDbo>(GameMvpCommonConsts.Collections.JOURNEY_DETAILS, pagedata.PageSize, pagedata.Skipped, filters);
            return _responseBuilder.SuccessPaggedData(game.ToList().ToJArray(), pagedata.CurrentPage, pagedata.PageSize);

        }


        #endregion journeydetails
    }
}
