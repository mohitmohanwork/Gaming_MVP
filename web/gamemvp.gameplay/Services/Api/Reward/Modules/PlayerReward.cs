using System;
using System.Collections.Generic;
using System.Text;

namespace gamemvp.gameplay.Services.Api.Reward.Modules
{
    class PlayerRewards
    {
        public string name { get; set; }
        public RewardTypes type { get; set; }
        public string value { get; set; }
        public DateTime created_on { get; set; }
    }

}
