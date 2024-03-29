﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ZNxt.Net.Core.Config;

namespace GameMVP.Web.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>

             WebHost.CreateDefaultBuilder(args)
               .UseKestrel(options =>
               {
                   var fileName = "ZNxtIdentitySigning.pfx";
                   var cert = new X509Certificate2(fileName, "abc@123");

                   options.Listen(IPAddress.Any, ApplicationConfig.HttpPort);
                   options.Listen(IPAddress.Any, ApplicationConfig.HttpsPort, listenOptions =>
                   {
                       listenOptions.UseHttps(cert);
                   });
               })
               .UseStartup<Startup>();
    }
}
