using System;
using System.Collections.Generic;
using System.Text;

namespace gamemvp.admin.Consts
{
    public static class AdminConsts
    {
        public const string SERVICE_NAME = "Admin Service";
        public const string SERVICE_API_PREFIX = "/admn";
        public static string GetServiceInfo()
        {
            return $"Name: {SERVICE_NAME}, Api Prefix: {SERVICE_API_PREFIX}";
        }
    }
}
