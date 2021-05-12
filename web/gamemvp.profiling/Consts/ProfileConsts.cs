using System;
using System.Collections.Generic;
using System.Text;

namespace gamemvp.profiling.Consts
{
    public static class ProfileConsts
    {
        public const string SERVICE_NAME = "ProfilingService";
        public const string SERVICE_API_PREFIX = "/uprs";
        public static string GetServiceInfo()
        {
            return $"Name: {SERVICE_NAME}, Api Prefix: {SERVICE_API_PREFIX}";
        }
    }
}
