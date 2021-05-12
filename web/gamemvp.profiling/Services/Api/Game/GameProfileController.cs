using gamemvp.profiling.Consts;
using gamemvp.profiling.Services.Api.Game.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ZNxt.Net.Core.Consts;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Interfaces;
using ZNxt.Net.Core.Model;
namespace gamemvp.profiling.Services.Api.Game
{

    public class GameProfileController : ZNxt.Net.Core.Services.ApiBaseService
    {
        private readonly IResponseBuilder _responseBuilder;
        private IDBService _dBService;
        private readonly IHttpContextProxy _httpContextProxy;
        private readonly ILogger _logger;
        private readonly IRDBService _rDBService;

        public GameProfileController(IHttpContextProxy httpContextProxy,
            IDBService dBService,
            IRDBService rDBService,
            ILogger logger, IResponseBuilder responseBuilder)
      : base(httpContextProxy, dBService, logger, responseBuilder)
        {
            _dBService = dBService;
            _httpContextProxy = httpContextProxy;
            _logger = logger;
            _responseBuilder = responseBuilder;
            _rDBService = rDBService;
        }

        [Route(ProfileConsts.SERVICE_API_PREFIX + "/games", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GetGames()
        {
            List<GameProfileModel> data = new List<GameProfileModel>()
            {
                new GameProfileModel(){ 
                        game_id = "7384",
                        name = "3 Luckey winner to get 50% off on iPhone",
                        campaign_id = "1425",
                        descriptoin = "3 Luckey winner to get 50% off on iPhone",
                        game_url = "https://gamevp.com/play/7384",
                        game_image= "https://gamevp.com/img/7384.jpg",
                        status = new GamePlayStatus(){
                             message = "You are very close to win",
                             progress = 70
                        },
                         play_summary = new GamePlaySummary(){
                            amount = 7583,
                            winners = 454
                        }

                },
                new GameProfileModel(){
                        game_id = "7845",
                        name = "Enjoy free spin for 3 months",
                        campaign_id = "6747",
                        descriptoin = "Enjoy free spin for 3 months",
                        game_url = "https://gamevp.com/play/7845",
                        game_image= "https://gamevp.com/img/7845.jpg",
                        status = new GamePlayStatus(){
                             message = "You are very close to win",
                             progress = 70
                        },
                        play_summary = new GamePlaySummary(){ 
                            amount = 3234,
                            winners = 245 
                        }
                },
                new GameProfileModel(){
                        game_id = "7465",
                        name = "Exclusive 18% off on all store items",
                        campaign_id = "3546",
                        descriptoin = "Exclusive 18% off on all store items",
                        game_url = "https://gamevp.com/play/7465",
                        game_image= "https://gamevp.com/img/7465.jpg",
                        status = new GamePlayStatus(){
                             message = "You are very close to win",
                             progress = 70
                        },
                         play_summary = new GamePlaySummary(){
                            amount = 7748,
                            winners = 645
                        }
                },
                new GameProfileModel(){
                        game_id = "7584",
                        name = "Jackpot rewards of INR 1000 on Next 10 Purchase" ,
                        campaign_id = "6374",
                        descriptoin = "Jackpot rewards of INR 1000 on Next 10 Purchase" ,
                        game_url = "https://gamevp.com/play/7584",
                        game_image= "https://gamevp.com/img/7584.jpg",
                        status = new GamePlayStatus(){
                             message = "You are very close to win",
                             progress = 70
                        },
                         play_summary = new GamePlaySummary(){
                            amount = 775,
                            winners = 28
                        }
                }
            };
            return _responseBuilder.SuccessPaggedData(data.ToJArray(), data.Count, 1);
        }
    }
}
