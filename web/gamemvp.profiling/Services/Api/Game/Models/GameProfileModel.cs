using System;
using System.Collections.Generic;
using System.Text;

namespace gamemvp.profiling.Services.Api.Game.Models
{
    public class GameProfileModel
    {
        public string game_id { get; set; }
        public string name { get; set; }
        public string descriptoin { get; set; }
        public string game_url { get; set; }
        public string game_image { get; set; }
        public GamePlayStatus status { get; set; }
        public GamePlaySummary play_summary { get; set; } 
        public string campaign_id { get; set; }
    }

}
