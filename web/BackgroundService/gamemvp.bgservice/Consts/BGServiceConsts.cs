using System;
using System.Collections.Generic;
using System.Text;

namespace gamemvp.bgservice.Consts
{
    public static class BGserviceConsts
    {
        public const string SERVICE_NAME = "Background Service";
        public const string SERVICE_API_PREFIX = "/bgsr";
        public static string GetServiceInfo()
        {
            return $"Name: {SERVICE_NAME}, Api Prefix: {SERVICE_API_PREFIX}";
        }
    }
}
