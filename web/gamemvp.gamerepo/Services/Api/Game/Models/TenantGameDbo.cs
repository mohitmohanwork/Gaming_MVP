using Dapper.Contrib.Extensions;
using gamemvp.common.Consts;
using gamemvp.common.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace gamemvp.gamerepo.Services.Api.Game.Models
{
   
    [Table(GameMvpCommonConsts.Collections.TENANT_GAMES)]
    public class TenantGameDbo : BaseDBModel
    {
        [Dapper.Contrib.Extensions.Key]
        public long tenant_games_id { get; set; }

        public long game_id { get; set; }

        public long tenant_id { get; set; }

        public bool is_active { get; set; } = true;


    }
}
