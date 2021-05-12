using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Interfaces;

namespace gamemvp.bgservice.Services
{
    public class Logger : ILogger
    {
        public string TransactionId  { get { return "tbx001"; } }

        public double TransactionStartTime { get { return CommonUtility.GetUnixTimestamp(DateTime.UtcNow); } }

        public void Debug(string message, JObject logData = null)
        {
            Console.WriteLine($"{message}");
        }

        public void Error(string message, Exception ex)
        {
            Console.WriteLine($"{message}");
        }

        public void Error(string message, Exception ex = null, JObject logData = null)
        {
            Console.WriteLine($"{message}");
        }

        public void Info(string message, JObject logData = null)
        {
            Console.WriteLine($"{message}");
        }

        public void Transaction(JObject transactionData, TransactionState state)
        {
            throw new NotImplementedException();
        }
    }
}
