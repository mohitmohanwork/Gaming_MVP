using gamemvp.campaign.Api;
using gamemvp.campaign.Services.Api.Campaign;
using gamemvp.campaign.Services.Api.Journey;
using gamemvp.campaign.Services.Api.Reward;
using gamemvp.testhelper.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using ZNxt.Net.Core.Interfaces;

namespace gamemvp.campaign.test.Helpers
{
    public static class ControllerHelper
    {
        public static PingController GetPingController(JToken httpRequestBody = null, Dictionary<string, string> querystring = null, Dictionary<string, string> headers = null)
        {
            var logger = new LoggerMock();
            var httpProxy = CommonExtensions.GetHttpProxyMock(httpRequestBody, querystring, headers);
            var responseBuilder = new ZNxt.Net.Core.Helpers.ResponseBuilder(logger, logger);
            return new PingController(httpProxy, CommonExtensions.GetDBService(httpProxy), GetRDBService(httpProxy), logger, responseBuilder);
        }

        internal static JourneyController GetJourneyController(JToken httpRequestBody = null, Dictionary<string, string> querystring = null, Dictionary<string, string> headers = null)
        {
            var logger = new LoggerMock();
            var httpProxy = CommonExtensions.GetHttpProxyMock(httpRequestBody, querystring, headers);
            var responseBuilder = new ZNxt.Net.Core.Helpers.ResponseBuilder(logger, logger);
            return new JourneyController(httpProxy, CommonExtensions.GetDBService(httpProxy), GetRDBService(httpProxy), logger, responseBuilder);
        }

        internal static RewardController GetRewardController(JToken httpRequestBody = null, Dictionary<string, string> querystring = null, Dictionary<string, string> headers = null)
        {
            var logger = new LoggerMock();
            var httpProxy = CommonExtensions.GetHttpProxyMock(httpRequestBody, querystring, headers);
            var responseBuilder = new ZNxt.Net.Core.Helpers.ResponseBuilder(logger, logger);
            return new RewardController(httpProxy, CommonExtensions.GetDBService(httpProxy), GetRDBService(httpProxy), logger, responseBuilder);
        }
        internal static CampaignController GetCampaignController(JToken httpRequestBody = null, Dictionary<string, string> querystring = null, Dictionary<string, string> headers = null)
        {
            var logger = new LoggerMock();
            var httpProxy = CommonExtensions.GetHttpProxyMock(httpRequestBody, querystring, headers);
            var responseBuilder = new ZNxt.Net.Core.Helpers.ResponseBuilder(logger, logger);
            return new CampaignController(httpProxy, CommonExtensions.GetDBService(httpProxy), GetRDBService(httpProxy), logger, responseBuilder);
        }

        private  static IRDBService GetRDBService(IHttpContextProxy httpProxy)
        {
            var rdp = CommonExtensions.GetRDBService(httpProxy);
            rdp.Init("NPGSQL", "Host=103.212.120.203;Username=root;Password=root;Database=engagement_v1");
            return rdp;
        }


    }
}
