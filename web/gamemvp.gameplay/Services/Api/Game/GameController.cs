using Blaash.Gaming.Service.GamePlay;
using Blaash.Gaming.Service.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using ZNxt.Net.Core.Consts;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Interfaces;
using ZNxt.Net.Core.Model;

namespace Blaash.Gaming.Service.Gameplay
{
    public class GameController :  ZNxt.Net.Core.Services.ApiBaseService
    {
        private readonly IResponseBuilder _responseBuilder;
        private IDBService _dBService;
        private readonly IHttpContextProxy _httpContextProxy;
        private readonly ILogger _logger;
        private readonly IRDBService _rDBService;

        public GameController(IHttpContextProxy httpContextProxy,
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

        [Route(GamePlayConsts.SERVICE_API_PREFIX + "/game/gameLaunch", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject OnGameLaunch(GameLaunchRequest gameLaunchRequest)
        {
            var errorMesage = new List<string>();
            try
            {
                /*Store the Request parameter. 
                 * Generate the game Session ID
                 * Return Response Object
                 * 
                 * In Case of Error - Pass the Correct Error Response Code from APIResponseCode
                 * TO DO - How Remove Cache files from Directory for Github
                 */


               return _responseBuilder.Success(new GameLaunchResponse().ToJObject());

            }

            catch (Exception ex)
            {
                _logger.Error("OnGameLaunch Failed", ex);
                //TO DO - HOW to get he Transaction ID
                errorMesage.Add("Server Error - Check Server Logs for Trasaction ");
                return _responseBuilder.CreateReponseWithError((int)ResponseErrorCode.ServerError, errorMesage);

            }

        }

        [Route(GamePlayConsts.SERVICE_API_PREFIX + "/game/gameLaunch", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject OnLevelComplete(GameLevelCompleteRequest gameLevelCompleteRequest)
        {
            var errorMesage = new List<string>();
            try
            {
                /* Check for Session ID if the Engagement is Active
                 * Store the Game Score, & Level Information
                 * Return Repose Object
                 * 
                 * In Case of Error - Pass the Correct Error Response Code from APIResponseCode
                 */


                return _responseBuilder.Success(new GameLevelCompleteResponse().ToJObject());

            }

            catch (Exception ex)
            {
                _logger.Error("OnLevelComplete Failed", ex);
                //TO DO - HOW to get he Transaction ID
                errorMesage.Add("Server Error - Check Server Logs for Trasaction ");
                return _responseBuilder.CreateReponseWithError((int)ResponseErrorCode.ServerError, errorMesage);

            }

        }
    }
}
