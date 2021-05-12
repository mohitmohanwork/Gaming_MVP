using gamemvp.profiling.Consts;
using gamemvp.profiling.Services.Api.Game.Models;
using gamemvp.profiling.Services.Api.Profile.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ZNxt.Net.Core.Consts;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Interfaces;
using ZNxt.Net.Core.Model;
namespace gamemvp.profiling.Services.Api.Game
{

    public class PlayerProfileController : ZNxt.Net.Core.Services.ApiBaseService
    {
        private readonly IResponseBuilder _responseBuilder;
        private IDBService _dBService;
        private readonly IHttpContextProxy _httpContextProxy;
        private readonly ILogger _logger;
        private readonly IRDBService _rDBService;

        public PlayerProfileController(IHttpContextProxy httpContextProxy,
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

        [Route(ProfileConsts.SERVICE_API_PREFIX + "/profile", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GetProfile()
        {
            return _responseBuilder.Success(
                new PlayerProfileModel()
                {
                    email = "mohit.mohan@gmail.com",
                    first_name = "Mohit",
                    last_name = "Mohan",
                    middle_name = "S",
                    user_id = "11010",
                    avatar_url = "https://gamemvp.com/images/ajhadjkhka.jpg"
                }.ToJObject());
        }
    }
}
