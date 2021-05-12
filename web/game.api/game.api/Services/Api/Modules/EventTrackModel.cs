using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace game.api.Services.Api.Modules
{
    public class EventTrackModel
    {
        public string id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 20, ErrorMessage = "client_id must be 20 chars")]
        public string client_id{ get; set; }
        public string user_id{ get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Invalid event_name, event_name min length 5 max 20")]
        public string event_name { get; set; }

        public List<EventProperty> properties = new List<EventProperty>();
    }

}
