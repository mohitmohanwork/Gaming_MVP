using System;
using System.Collections.Generic;
using System.Text;

namespace Blaash.Gaming.Service.Common.Models
{
    public class BaseDBModel
    {

        public string created_by { get; set; }
        public long created_on { get; set; }
        public long updated_on { get; set; }
        public string updated_by { get; set; }


        public BaseDBModel()
        {
            updated_on = created_on = ZNxt.Net.Core.Helpers.CommonUtility.GetUnixTimestamp(DateTime.UtcNow);
        }

    }
}
