using System;
using System.Collections.Generic;
using System.Text;

namespace game.api.Consts
{
    public static class GameApiConsts
    {
        public const string SERVICE_NAME = "GameAPI";
        public const string SERVICE_API_PREFIX = "/gamp";
        public static string GetServiceInfo()
        {
            return $"Name: {SERVICE_NAME}, Api Prefix: {SERVICE_API_PREFIX}";
        }
    }
}
