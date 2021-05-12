using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blaash.Gaming.Service.Common.Models
{
    public class EventTrackModel
    {
        public string id { get; set; }
        [Required]
        [Range(1000, 9999, ErrorMessage = "client_id must be 4 digits")]
        public long client_id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Invalid event_name, event_name min length 5 max 20")]
        public string event_name { get; set; }

        public List<EventProperty> properties = new List<EventProperty>();

        public bool is_processed { get; set; } = false;
    }

}
