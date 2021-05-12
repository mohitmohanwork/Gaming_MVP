using System;
using System.Collections.Generic;
using System.Text;

namespace gamemvp.profiling.Services.Api.Game.Models
{
    public class GamePlayStatus
    {
        public string message { get; set; }
        public int progress { set; get; } // progress in %
    }
}
