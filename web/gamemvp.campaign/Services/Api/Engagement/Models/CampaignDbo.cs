using System.ComponentModel.DataAnnotations;
using Blaash.Gaming.Service.Common.Constants;
using Dapper.Contrib.Extensions;
using gamemvp.common.Models.Base;

namespace gamemvp.campaign.Services.Api.Campaign.Models
{

    [Table(GameMvpCommonConsts.Collections.ENGAGEMENT)]
    public class EngagementDbo : BaseDBModel
    {
        [Dapper.Contrib.Extensions.Key]
        public long engagement_id { get; set; }

        [Required(ErrorMessage = "tenant_id is  required")]
        public long tenant_id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Campaign display_name min length 5 max length 100")]
        public string display_name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Campaign dynamic_name min length 5 max length 100")]
        public string dynamic_name { get; set; }


        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Campaign short_name min length 3 max length 100")]
        public string short_name { get; set; }


        [Required(ErrorMessage = "startdate required Unix timestamp")]
        public long  startdate { get; set; }

        public long enddate { get; set; } = 0;

        [Required(ErrorMessage = "purchase_rule_id is required")]
        public long purchase_rule_id { get; set; }

        //[Required(ErrorMessage = "customer_segment_id is required")]
        //public long customer_segment_id { get; set; }

        [Required(ErrorMessage = "engagement_status_id is  required")]
        public long engagement_status_id { get; set; } = 1;

        public bool is_tournament_type { get; set; } = false;

        public long budget_per_day { get; set; } = 0;

        public long budget_days { get; set; } = 0;

        public string description { get; set; } = "";
    }
}
