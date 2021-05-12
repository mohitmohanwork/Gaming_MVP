using game.api.Services.Api;
using gamemvp.testhelper.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using ZNxt.Net.Core.DB.Mongo;
using ZNxt.Net.Core.Interfaces;

namespace em.ui.test.Helpers
{
    public static class ControllerHelper
    {

      
        public static TrackController GetTrackController(JToken httpRequestBody = null, Dictionary<string, string> querystring = null, Dictionary<string, string> headers = null)
        {
            var logger = new LoggerMock();
            var httpProxy = CommonExtensions.GetHttpProxyMock(httpRequestBody, querystring, headers);
            var responseBuilder = new ZNxt.Net.Core.Helpers.ResponseBuilder(logger, logger);
            return new TrackController(httpProxy, CommonExtensions.GetDBService(httpProxy), CommonExtensions.GetRDBService(httpProxy), logger, responseBuilder);
        }
       
    }
}
