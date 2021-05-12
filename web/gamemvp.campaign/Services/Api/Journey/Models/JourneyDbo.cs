using System.ComponentModel.DataAnnotations;
using Blaash.Gaming.Service.Common.Constants;
using Dapper.Contrib.Extensions;
using gamemvp.common.Models.Base;

namespace gamemvp.campaign.Services.Api.Journey.Models
{

    [Table(GameMvpCommonConsts.Collections.JOURNEY)]
    public class JourneyDbo : BaseDBModel
    {
        [Dapper.Contrib.Extensions.Key]
        public long journey_id { get; set; }
        [Required]
        public long tenant_id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Journey Name min length 5 max length 30")]
        public string name { get; set; }
        [Required]
        public long event_Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "event_display_name Name min length 5 max length 30")]
        public string event_display_name { get; set; }

    }
}
