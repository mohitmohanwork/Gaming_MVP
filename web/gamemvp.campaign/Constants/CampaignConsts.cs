using System;
using System.Collections.Generic;
using System.Text;

namespace gamemvp.campaign.Consts
{
    public static class CampaignConsts
    {
        public const string SERVICE_NAME = "Engagement";
        public const string SERVICE_API_PREFIX = "/engt";
        public static string GetServiceInfo()
        {
            return $"Name: {SERVICE_NAME}, Api Prefix: {SERVICE_API_PREFIX}";
        }
    }
}
