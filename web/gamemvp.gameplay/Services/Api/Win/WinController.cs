using gamemvp.gameplay.Consts;
using gamemvp.gameplay.Services.Api.Win.Modules;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using ZNxt.Net.Core.Consts;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Interfaces;
using ZNxt.Net.Core.Model;

namespace gamemvp.gameplay.Services.Api.Win
{
    public class WinController :  ZNxt.Net.Core.Services.ApiBaseService
    {
        private readonly IResponseBuilder _responseBuilder;
        private IDBService _dBService;
        private readonly IHttpContextProxy _httpContextProxy;
        private readonly ILogger _logger;
        private readonly IRDBService _rDBService;

        public WinController(IHttpContextProxy httpContextProxy,
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

        [Route(GamePlayConsts.SERVICE_API_PREFIX + "/win/transactions", CommonConst.ActionMethods.GET, CommonConst.CommonValue.ACCESS_ALL)]
        public JObject GeWinTransactions()
        {
            var data = new List<PlayerWin>() {
                     new PlayerWin(){
                          win_id = "23413",
                          name = "Rs 100 off",
                          type = WinTypes.voucher,
                          description = "Rs 100 off on any purchase",
                          voucher = new VoucherWinModel(){
                             code = "HAPPYNEWYEAR100",
                             expiry = DateTime.UtcNow.AddDays(3)
                          },
                          is_claimed = false,
                          campaign_id = "26537",
                          created_on = DateTime.UtcNow.AddDays(-2)
                     }
                };
            return _responseBuilder.SuccessPaggedData(data.ToJArray(), 1);
        }
    }
}
