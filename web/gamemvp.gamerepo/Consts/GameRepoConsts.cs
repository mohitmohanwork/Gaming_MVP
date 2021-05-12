using System;
using System.Collections.Generic;
using System.Text;

namespace gamemvp.gamerepo.Consts
{
    public static class GameRepoConsts
    {
        public const string SERVICE_NAME = "GameRepo";
        public const string SERVICE_API_PREFIX = "/gars";
        public static string GetServiceInfo()
        {
            return $"Name: {SERVICE_NAME}, Api Prefix: {SERVICE_API_PREFIX}";
        }
    }
}
