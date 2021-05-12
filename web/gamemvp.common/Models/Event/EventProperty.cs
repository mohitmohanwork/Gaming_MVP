using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blaash.Gaming.Service.Common.Models
{
    public class EventProperty
    {
        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Invalid EventProperty key")]
        public string key { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Invalid EventProperty value")]
        public string value { get; set; }

    }
}
