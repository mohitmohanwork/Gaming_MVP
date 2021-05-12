using gamemvp.common.Consts;
using gamemvp.common.Services;
using gamemvp.gamerepo.Consts;
using gamemvp.gamerepo.Services.Api.Game.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using ZNxt.Net.Core.Consts;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Interfaces;
using ZNxt.Net.Core.Model;

namespace gamemvp.gamerepo.Services.Api.Game
{
   public  class GameController : MvpBaseController
    {

        public GameController(IHttpContextProxy httpContextProxy,
            IDBService dBService,
            IRDBService rDBService,
            ILogger logger, IResponseBuilder responseBuilder)
      : base(httpContextProxy, dBService, rDBService, logger, responseBuilder)
        {

        }
        [Route(GameRepoConsts.SERVICE_API_PREFIX + "/addgame", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject AddGame()
        {
            var request = GetRequestBody<GameDbo>();
            var results = new Dictionary<string, string>();
            _logger.Debug("Validation model");
            if (request.IsValidModel(out results))
            {
                if (_rDBService.GetCount(GameMvpCommonConsts.Collections.GAME, new JObject() { ["name"] = request.name }) == 0)
                {
                   
                        SetUser(request);
                        var id = _rDBService.WriteData<GameDbo>(request);
                        request.game_id = id;
                        return _responseBuilder.Success(request.ToJObject());
                }
                else
                {
                    JObject errors = new JObject();
                    errors["key"] = $"Duplicate game name {request.name}";
                    return _responseBuilder.BadRequest(errors);
                }
            }
            else
            {
                return ModelValidationFailResponse(results);
            }
        }
        [Route(GameRepoConsts.SERVICE_API_PREFIX + "/updategame", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject UpdateGame()
        {
            var request = GetRequestBody<GameDbo>();
            var results = new Dictionary<string, string>();
            _logger.Debug("Validation model");
            if (request.IsValidModel(out results))
            {
                if (_rDBService.GetCount(GameMvpCommonConsts.Collections.GAME, new JObject() { [nameof(request.game_id)] = request.game_id  }) != 0)
                {

                    SetUser(request);
                    var result = _rDBService.Update<GameDbo>(request);
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
                    errors["key"] = $"Game not found, game id: {request.game_id}";
                    return _responseBuilder.NotFound(errors,request.ToJObject());
                }
            }
            else
            {
                return ModelValidationFailResponse(results);
            }
        }
        [Route(GameRepoConsts.SERVICE_API_PREFIX + "/gamebyid", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GetGameById()
        {
            var gameid = _httpContextProxy.GetQueryString("game_id");
            long ingameid = 0;
            if (!string.IsNullOrEmpty(gameid) && long.TryParse(gameid, out ingameid))
            {
                var game = _rDBService.Get<GameDbo>(GameMvpCommonConsts.Collections.GAME,1,0,new JObject() { ["game_id"] = gameid });
                if (game.Any())
                {
                    return _responseBuilder.Success(game.First().ToJObject());
                }
                else
                {
                    JObject errors = new JObject();
                    errors["key"] = $"Game not found, game id {gameid}";
                    return _responseBuilder.NotFound(errors);
                }
            }
            else
            {
                JObject errors = new JObject();
                errors["key"] = $"Bad request invalid game id {gameid}";
                return _responseBuilder.BadRequest(errors);
            }
        }
        [Route(GameRepoConsts.SERVICE_API_PREFIX + "/gamebyfilters", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GetGameByFilter()
        {

            var filters = GetFiltersFromQueryString();
            var pagedata = GetRequestPaggedData();
            var game = _rDBService.Get<GameDbo>(GameMvpCommonConsts.Collections.GAME, pagedata.PageSize, pagedata.Skipped, filters);

            return _responseBuilder.SuccessPaggedData(game.ToList().ToJArray(), pagedata.CurrentPage, pagedata.PageSize);

        }


        [Route(GameRepoConsts.SERVICE_API_PREFIX + "/addtenantgame", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject AddTenantGame()
        {
            var request = GetRequestBody<TenantGameDbo>();
            var results = new Dictionary<string, string>();
            _logger.Debug("Validation model");
            if (request.IsValidModel(out results))
            {
                if (_rDBService.GetCount(GameMvpCommonConsts.Collections.TENANT_GAMES, new JObject() { [nameof(request.game_id)] = request.game_id  , [nameof(request.is_active)] = true}) == 0)
                {

                    SetUser(request);
                    var id = _rDBService.WriteData<TenantGameDbo>(request);
                    request.tenant_games_id = id;
                    return _responseBuilder.Success(request.ToJObject());
                }
                else
                {
                    JObject errors = new JObject();
                    errors["key"] = $"Duplicate record";
                    return _responseBuilder.BadRequest(errors, request.ToJObject());
                }
            }
            else
            {
                return ModelValidationFailResponse(results);
            }
        }
        [Route(GameRepoConsts.SERVICE_API_PREFIX + "/updatetenantgame", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject UpdateTenantGame()
        {
            var request = GetRequestBody<TenantGameDbo>();
            var results = new Dictionary<string, string>();
            _logger.Debug("Validation model");
            if (request.IsValidModel(out results))
            {
                if (_rDBService.GetCount(GameMvpCommonConsts.Collections.TENANT_GAMES, new JObject() { [nameof(request.tenant_games_id)] = request.tenant_games_id}) != 0)
                {

                    SetUser(request);
                    var result = _rDBService.Update<TenantGameDbo>(request);
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
                    errors["key"] = $"Game tenent not found, id: {request.tenant_games_id}";
                    return _responseBuilder.NotFound(errors, request.ToJObject());
                }
            }
            else
            {
                return ModelValidationFailResponse(results);
            }
        }


        [Route(GameRepoConsts.SERVICE_API_PREFIX + "/gettenantgame", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GetTenantGame()
        {
            var tenantid = _httpContextProxy.GetQueryString("tenant_id");
            var results = new Dictionary<string, string>();
            _logger.Debug("Validation model");
            long ingameid = 0;
            if (!string.IsNullOrEmpty(tenantid) && long.TryParse(tenantid, out ingameid))
            {
                var pagedata = GetRequestPaggedData();
                var game = _rDBService.Get<GameDbo>(@"
                                                        select g.* from game as g 
                                                        right outer join tenant_games tg on (tg.game_id = g.game_id)
                                                        where tg.tenant_id = @tenant_id
                                                        ", new { tenant_id = ingameid });

                return _responseBuilder.SuccessPaggedData(game.ToList().ToJArray(), pagedata.CurrentPage, pagedata.PageSize);
            }
            else
            {
                JObject errors = new JObject();
                errors["key"] = $"Bad request invalid tenant id {tenantid}";
                return _responseBuilder.BadRequest(errors);
            }
        }
    }
}
