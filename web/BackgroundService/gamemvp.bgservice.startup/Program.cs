using gamemvp.bgservice.Services;
using Microsoft.Extensions.Caching.Memory;
using System;
using ZNxt.Net.Core.DB.Mongo;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Interfaces;
using ZNxt.Net.Core.Web.Services;

namespace gamemvp.bgservice.startup
{
    class Program
    {
        static void Main(string[] args)
        {
            IHttpContextProxy httpProxy = new HttpContextProxyMock();
            ILogger logger = new Services.Logger();
            IInMemoryCacheService cache = new InMemoryCacheService(new MemoryCache(new MemoryCacheOptions()));
            IApiGatewayService apiGateway = new ApiGatewayService(cache, httpProxy, logger);
            var controller = new EventsScanController(GetDBService(httpProxy), logger, apiGateway, cache);
            controller.ProcessEvent();
            Console.WriteLine();
            Console.ReadLine();
        }

        public static MongoDBService GetDBService(IHttpContextProxy httpContextProxy)
        {
            var dbconfig = new ZNxt.Net.Core.DB.Mongo.MongoDBServiceConfig();
            dbconfig.Set(CommonUtility.GetAppConfigValue("DataBaseName"), CommonUtility.GetAppConfigValue("ConnectionString"));
            var dBService = new ZNxt.Net.Core.DB.Mongo.MongoDBService(dbconfig, httpContextProxy);
            return dBService;
        }
    }
}
