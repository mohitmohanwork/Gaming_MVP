using System.Collections.Generic;
using System.Linq;
using Blaash.Gaming.Service.Common.Constants;
using gamemvp.campaign.Consts;
using gamemvp.campaign.Services.Api.Reward.Models;
using gamemvp.common.Services;
using Newtonsoft.Json.Linq;
using ZNxt.Net.Core.Consts;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Interfaces;
using ZNxt.Net.Core.Model;

namespace gamemvp.campaign.Services.Api.Reward
{
    public  class RewardController : MvpBaseController
    {

        public RewardController(IHttpContextProxy httpContextProxy,
            IDBService dBService,
            IRDBService rDBService,
            ILogger logger, IResponseBuilder responseBuilder)
      : base(httpContextProxy, dBService, rDBService, logger, responseBuilder)
        {

        }

        #region reward 
        [Route(CampaignConsts.SERVICE_API_PREFIX + "/addreward", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject AddReward()
        {
            var request = GetRequestBody<RewardDbo>();
            return AddReward(request);
        }
        [Route(CampaignConsts.SERVICE_API_PREFIX + "/addupdatereward", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject AddUpdateReward()
        {
            var request = GetRequestBody<RewardDbo>();
            if (request.reward_id == 0)
            {
                return AddReward(request);
            }
            else
            {
                return UpdateRewards(request);
            }
        }

        private JObject AddReward(RewardDbo request)
        {
            var results = new Dictionary<string, string>();
            _logger.Debug("Validation model");
            if (request.IsValidModel(out results))
            {
                if (_rDBService.GetCount(GameMvpCommonConsts.Collections.REWARD, new JObject() { [nameof(request.name)] = request.name, [nameof(request.tenant_id)] = request.tenant_id }) == 0)
                {

                    SetUser(request);
                    var id = _rDBService.WriteData<RewardDbo>(request);
                    request.reward_id = id;
                    return _responseBuilder.Success(request.ToJObject());
                }
                else
                {
                    JObject errors = new JObject();
                    errors["key"] = $"Duplicate reward name {request.name},  tenant_id: {request.tenant_id}";
                    return _responseBuilder.BadRequest(errors);
                }
            }
            else
            {
                return ModelValidationFailResponse(results);
            }
        }

        [Route(CampaignConsts.SERVICE_API_PREFIX + "/updatereward", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject UpdateReward()
        {
            var request = GetRequestBody<RewardDbo>();
            return UpdateRewards(request);
        }

        private JObject UpdateRewards(RewardDbo request)
        {
            var results = new Dictionary<string, string>();
            _logger.Debug("Validation model");
            if (request.IsValidModel(out results))
            {
                if (_rDBService.GetCount(GameMvpCommonConsts.Collections.REWARD, new JObject() { [nameof(request.reward_id)] = request.reward_id }) != 0)
                {

                    SetUser(request);
                    var result = _rDBService.Update<RewardDbo>(request);
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
                    errors["key"] = $"Reward not found,  id: {request.reward_id}";
                    return _responseBuilder.NotFound(errors, request.ToJObject());
                }
            }
            else
            {
                return ModelValidationFailResponse(results);
            }
        }

        [Route(CampaignConsts.SERVICE_API_PREFIX + "/rewardbyid", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GetRewardById()
        {
            var rewardid = _httpContextProxy.GetQueryString("reward_id");
            long ingameid = 0;
            if (!string.IsNullOrEmpty(rewardid) && long.TryParse(rewardid, out ingameid))
            {
                var journey = _rDBService.Get<RewardDbo>(GameMvpCommonConsts.Collections.REWARD,1,0,new JObject() { ["reward_id"] = rewardid });
                if (journey.Any())
                {
                    return _responseBuilder.Success(journey.First().ToJObject());
                }
                else
                {
                    JObject errors = new JObject();
                    errors["key"] = $"reward not found, id {rewardid}";
                    return _responseBuilder.NotFound(errors);
                }
            }
            else
            {
                JObject errors = new JObject();
                errors["key"] = $"Bad request invalid reward id {rewardid}";
                return _responseBuilder.BadRequest(errors);
            }
        }
        [Route(CampaignConsts.SERVICE_API_PREFIX + "/rewardbyfilters", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GetRewardByFilter()
        {

            var filters = GetFiltersFromQueryString();
            var pagedata = GetRequestPaggedData();
            var game = _rDBService.Get<RewardDbo>(GameMvpCommonConsts.Collections.REWARD, pagedata.PageSize, pagedata.Skipped, filters);

            return _responseBuilder.SuccessPaggedData(game.ToList().ToJArray(), pagedata.CurrentPage, pagedata.PageSize);

        }

        #endregion 


    }
}
