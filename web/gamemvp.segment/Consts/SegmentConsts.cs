using System;
using System.Collections.Generic;
using System.Text;

namespace gamemvp.segment.Consts
{
    public static class SegmentConsts
    {
        public const string SERVICE_NAME = "segment Service";
        public const string SERVICE_API_PREFIX = "/engt";
        public static string GetServiceInfo()
        {
            return $"Name: {SERVICE_NAME}, Api Prefix: {SERVICE_API_PREFIX}";
        }
    }
}
