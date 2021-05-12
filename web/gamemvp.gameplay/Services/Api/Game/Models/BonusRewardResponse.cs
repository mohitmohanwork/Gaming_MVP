using System;
namespace Blaash.Gaming.Service.GamePlay
{
    public class BonusRewardResponse
    {
       public int gameSessionID { get; set; }

        public RewardItem rewardItem { get; set; }

        public int nextRewadLevel { get; set; }

    }
}
