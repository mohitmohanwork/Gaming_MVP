using System;
using System.Collections.Generic;
using System.Text;

namespace gamemvp.gameplay.Services.Api.Reward.Modules
{
    class RewardSummaryModel
    {
        public int points { get; set; }
        public int amount { get; set; }
        public int rewards { get; set; }
        public int rank { get; set; }
        public int wins { get; set; }
        public int passedaway { get; set; }

    }
}
