using System;
using System.Collections.Generic;
using System.Text;
using ZNxt.Net.Core.Interfaces;

namespace Blaash.Gaming.Service.Common.Models
{
   public  class APIResponseCode : IMessageCodeContainer
    { 
        private Dictionary<int, string> _text = new Dictionary<int, string>();

        public string Get(int code)
        {
            if (_text.ContainsKey(code))
            {
                return _text[code];
            }
            else
            {
                return string.Empty;
            }
        }

        public APIResponseCode()
        {
            _text[(int)ResponseErrorCode.ALREADY_EXISTS] = "ALREADY_EXISTS";
        }
    }
}
