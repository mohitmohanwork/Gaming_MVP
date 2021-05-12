using game.api.Consts;
using game.api.Services.Api.Modules;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ZNxt.Net.Core.Consts;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Interfaces;
using ZNxt.Net.Core.Model;
namespace game.api.Services.Api
{
    public class TrackController : ZNxt.Net.Core.Services.ApiBaseService
    {
        private readonly IResponseBuilder _responseBuilder;
        private IDBService _dBService;
        private readonly IHttpContextProxy _httpContextProxy;
        private readonly ILogger _logger;
        private readonly IRDBService _rDBService;

        public TrackController(IHttpContextProxy httpContextProxy,
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

        [Route(GameApiConsts.SERVICE_API_PREFIX + "/mock", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject Getmock()
        {
          //   _rDBService.WriteData("INSERT INTO [dbo].[Event]([Id] ,[EventLocationId],[EventName])VALUES(999, 99, '99')");
            return _responseBuilder.Success(new JObject() { ["name"] = "game1" });
        }

        [Route(GameApiConsts.SERVICE_API_PREFIX+"/pushevent", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject trackevent()
        {
            try
            {
                var request = _httpContextProxy.GetRequestBody<EventTrackModel>();
                _logger.Debug("Validation model");
                var results = new Dictionary<string, string>();
                if (request.IsValidModel(out results))
                {
                    foreach (var prop in request.properties)
                    {
                        if (!prop.IsValidModel(out results)) 
                        {
                            return ModelValidationFailResponse(results);
                        }
                    }
                    request.id = ZNxt.Net.Core.Helpers.CommonUtility.GetNewID();
                    if (_dBService.WriteData("track", request.ToJObject()))
                    {
                        return _responseBuilder.Success();
                    }
                    else
                    {
                        return _responseBuilder.ServerError();
                    }
                }
                else
                {
                    return ModelValidationFailResponse(results);
                }
            }
            catch (System.Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return _responseBuilder.ServerError();
            }
        }

        private JObject ModelValidationFailResponse(Dictionary<string, string> results)
        {
            _logger.Debug("Model validation fail");
            JObject errors = new JObject();
            foreach (var error in results)
            {
                errors[error.Key] = error.Value;
            }
            return _responseBuilder.BadRequest(errors);
        }

        [Route(GameApiConsts.SERVICE_API_PREFIX + "/event", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GetEvent()
        {
            return GetPaggedData("track");
        }
    }
}
