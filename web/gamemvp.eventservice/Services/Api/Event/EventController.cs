using System.Collections.Generic;
using gamemvp.eventservice.Consts;
using Newtonsoft.Json.Linq;
using ZNxt.Net.Core.Consts;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Interfaces;
using ZNxt.Net.Core.Model;
using Blaash.Gaming.Service.Common;
using Blaash.Gaming.Service.Common.Models;
using Neo4jClient;
using System;

namespace gamemvp.eventservice.Services.Api.Event
{
    public class EventController : ZNxt.Net.Core.Services.ApiBaseService
    {

        private readonly IResponseBuilder _responseBuilder;
        private IDBService _dBService;
        private readonly IHttpContextProxy _httpContextProxy;
        private readonly ILogger _logger;
        private readonly IRDBService _rDBService;
       
         public EventController(IHttpContextProxy httpContextProxy,
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
        [Route(EventConsts.SERVICE_API_PREFIX + "/pushevent", CommonConst.ActionMethods.POST, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject PushEvent()
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
                    ProcessEventsData("test", new List<EventProperty>());
                    request.id = ZNxt.Net.Core.Helpers.CommonUtility.GetNewID();
                    if (_dBService.WriteData(GameMvpCommonConsts.Collections.EVENT, request.ToJObject()))
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

        [Route(EventConsts.SERVICE_API_PREFIX + "/event", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GetEvent()
        {
            return GetPaggedData(GameMvpCommonConsts.Collections.EVENT);
        }

        private void ProcessEventsData(string event_name, List<EventProperty> eventProperties)
        {
            var neo4jClient = new GraphClient(new Uri("http://localhost:7474/db/data"), "Neo4j", "abc@1234");
            neo4jClient.ConnectAsync();

        }
    }
}
