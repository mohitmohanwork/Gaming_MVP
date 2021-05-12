using System;
using System.Collections.Generic;
using System.Text;

namespace Blaash.Gaming.Service.Common
{
    public class RequestPageData
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int Skipped { get; set; }
    }
}
