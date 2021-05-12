using System;
using System.Collections.Generic;
using System.Text;

namespace gamemvp.tenantmgmt.Consts
{
    public static class TenantMgmtConsts
    {
        public const string SERVICE_NAME = "Tenant Management";
        public const string SERVICE_API_PREFIX = "/tenm";
        public static string GetServiceInfo()
        {
            return $"Name: {SERVICE_NAME}, Api Prefix: {SERVICE_API_PREFIX}";
        }
    }
}
