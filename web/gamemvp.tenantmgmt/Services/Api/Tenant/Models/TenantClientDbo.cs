using Dapper.Contrib.Extensions;
using gamemvp.common.Consts;
using gamemvp.common.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace gamemvp.tenantmgmt.Services.Api.Tenant.Modules
{
    [Table(GameMvpCommonConsts.Collections.TENANT_CLIENT)]
    public class TenantClientDbo : BaseDBModel
    {
        [Dapper.Contrib.Extensions.Key]
        public long id { get; set; }

        public string client_id { get; set; }
        public long tenant_id { get; set; }
        public bool is_active { get; set; } = true;

    }
}
