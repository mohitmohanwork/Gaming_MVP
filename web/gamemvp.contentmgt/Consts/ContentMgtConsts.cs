using System;
using System.Collections.Generic;
using System.Text;

namespace gamemvp.contentmgt.Consts
{
    public static class ContentMgtConsts
    {
        public const string SERVICE_NAME = "Content-Mgt";
        public const string SERVICE_API_PREFIX = "/ctms";
        public static string GetServiceInfo()
        {
            return $"Name: {SERVICE_NAME}, Api Prefix: {SERVICE_API_PREFIX}";
        }
    }
}
