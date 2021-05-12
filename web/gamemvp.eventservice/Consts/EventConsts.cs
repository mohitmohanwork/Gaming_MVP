using System;
using System.Collections.Generic;
using System.Text;

namespace gamemvp.eventservice.Consts
{
    public static class EventConsts
    {
        public const string SERVICE_NAME = "EventService";
        public const string SERVICE_API_PREFIX = "/evnt";
        public static string GetServiceInfo()
        {
            return $"Name: {SERVICE_NAME}, Api Prefix: {SERVICE_API_PREFIX}";
        }
    }
}
