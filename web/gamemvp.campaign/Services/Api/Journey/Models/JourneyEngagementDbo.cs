using System.ComponentModel.DataAnnotations;
using Blaash.Gaming.Service.Common.Constants;
using Dapper.Contrib.Extensions;
using gamemvp.common.Models.Base;

namespace gamemvp.campaign.Services.Api.Journey.Models
{

    [Table(GameMvpCommonConsts.Collections.ENGAGEMENT_JOURNEY)]
    public class JourneyEngagementDbo : BaseDBModel
    {
        [Dapper.Contrib.Extensions.Key]
        public long engagement_journey_id { get; set; }
        public long journey_id { get; set; }
        public long engagement_id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name min length 5 max length 50")]
        public string name { get; set; }

        public bool is_active { get; set; } = true;

        public long value { get; set; } = 0;

        public long order { get; set; } = 0;
    }
}
