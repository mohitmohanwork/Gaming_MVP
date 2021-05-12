using System;
using System.Collections.Generic;
using System.Text;

namespace gamemvp.profiling.Services.Api.Profile.Models
{
    class PlayerProfileModel
    {
        public string  user_id { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string email{ get; set; }

        public string avatar_url { get; set; }

    }
}
