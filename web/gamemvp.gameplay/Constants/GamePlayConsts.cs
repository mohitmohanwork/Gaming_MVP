using System;
using System.Collections.Generic;
using System.Text;

namespace Blaash.Gaming.Service.GamePlay
{
    public static class GamePlayConsts
    {
        public const string SERVICE_NAME = "GamePlay";
        public const string SERVICE_API_PREFIX = "/gply";
        public static string GetServiceInfo()
        {
            return $"Name: {SERVICE_NAME}, Api Prefix: {SERVICE_API_PREFIX}";
        }
    }
}
