using Blaash.Gaming.Service.Common.Constants;
using Dapper.Contrib.Extensions;
using gamemvp.common.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace gamemvp.campaign.Services.Api.Reward.Models
{

    [Table(GameMvpCommonConsts.Collections.REWARD)]
    public class RewardDbo : BaseDBModel
    {
        [Dapper.Contrib.Extensions.Key]
        public long reward_id { get; set; }

        public long tenant_id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Reward Name min length 5 max length 150")]
        public string name { get; set; }

        public string description { get; set; }
    }
}
