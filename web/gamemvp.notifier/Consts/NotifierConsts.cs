using System;
using System.Collections.Generic;
using System.Text;

namespace gamemvp.notifier.Consts
{
    public static class NotifierConsts
    {
        public const string SERVICE_NAME = "Notifier";
        public const string SERVICE_API_PREFIX = "/nots";
        public static string GetServiceInfo()
        {
            return $"Name: {SERVICE_NAME}, Api Prefix: {SERVICE_API_PREFIX}";
        }
    }
}
