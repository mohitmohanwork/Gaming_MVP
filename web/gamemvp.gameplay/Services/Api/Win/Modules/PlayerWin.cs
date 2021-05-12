using System;
using System.Collections.Generic;
using System.Text;

namespace gamemvp.gameplay.Services.Api.Win.Modules
{
    class PlayerWin
    {

        public string win_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public WinTypes type { get; set; }
        public string campaign_id { get; set; }
        public bool is_claimed { get; set; }
        public VoucherWinModel voucher { get; set; }
        public DateTime created_on { get; set; }
    }

}
