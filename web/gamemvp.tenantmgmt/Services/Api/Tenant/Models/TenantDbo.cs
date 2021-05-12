using Dapper.Contrib.Extensions;
using gamemvp.common.Consts;
using gamemvp.common.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace gamemvp.tenantmgmt.Services.Api.Tenant.Modules
{
    [Table(GameMvpCommonConsts.Collections.TENANT)]
    public  class TenantDbo : BaseDBModel
    {
        [Dapper.Contrib.Extensions.Key]
        public long tenant_id { get; set; }
       
        [Required]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Tenant Name min length 5 max length 150")]
        public string name { get; set; }
        [Required]
        public TenantStatus status { get; set; }
        [Required]
        public string description { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Email min length 5 max 150")]
        public string email { get; set; }
        public string phone_number { get; set; }
    }
}
