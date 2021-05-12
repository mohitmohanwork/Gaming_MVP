using Blaash.Gaming.Service.Common.Constants;
using Dapper.Contrib.Extensions;
using gamemvp.common.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace gamemvp.campaign.Services.Api.Journey.Models
{
   
    [Table(GameMvpCommonConsts.Collections.JOURNEY_DETAILS)]
    public class JourneyDetailsDbo : BaseDBModel
    {
        [Dapper.Contrib.Extensions.Key]
        public long journey_detail_id { get; set; }

        public long journey_id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Journey details Name min length 5 max length 150")]
        public string name { get; set; }

        public string description { get; set; }
    }
}
