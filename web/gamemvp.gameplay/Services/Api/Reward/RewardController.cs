using gamemvp.gameplay.Consts;
using gamemvp.gameplay.Services.Api.Reward.Modules;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using ZNxt.Net.Core.Consts;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Interfaces;
using ZNxt.Net.Core.Model;
namespace gamemvp.gameplay.Api
{
    public class RewardController : ZNxt.Net.Core.Services.ApiBaseService
    {
        private readonly IResponseBuilder _responseBuilder;
        private IDBService _dBService;
        private readonly IHttpContextProxy _httpContextProxy;
        private readonly ILogger _logger;
        private readonly IRDBService _rDBService;

        public RewardController(IHttpContextProxy httpContextProxy,
            IDBService dBService,
            IRDBService rDBService,
            ILogger logger,IResponseBuilder responseBuilder)
      : base(httpContextProxy, dBService, logger, responseBuilder)
        {
            _dBService = dBService;
            _httpContextProxy = httpContextProxy;
            _logger = logger;
            _responseBuilder = responseBuilder;
            _rDBService = rDBService;
        }

        [Route(GamePlayConsts.SERVICE_API_PREFIX + "/rewards", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GetRewards()
        {
           
            return _responseBuilder.Success(new RewardModel()
            {
                summary = new RewardSummaryModel() { 
                    points = 2120,
                    rewards = 5,
                    rank = 12,
                    amount = 3234,
                    wins = 5,
                    passedaway = 1645
                }
            }.ToJObject());
        }
     
        [Route(GamePlayConsts.SERVICE_API_PREFIX + "/points/transactions", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GetPointTransactions()
        {
            var data = new List<PlayerRewards>() {
                     new PlayerRewards(){
                          name = "Points",
                          type = RewardTypes.points,
                          value = "2120",
                          created_on = DateTime.UtcNow.AddDays(-2)
                     }
                };
            return _responseBuilder.SuccessPaggedData(data.ToJArray(), 1);
        }
    }
}
