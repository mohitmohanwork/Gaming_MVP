﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.HttpOverrides;
using ZNxt.Net.Core.Helpers;
using ZNxt.Net.Core.Consts;
using System;
using gamemvp.eventservice.Consts;
using ZNxt.Net.Core.Web.Services;
using ZNxt.Net.Core.Interfaces;

namespace GameMVP.Web.Test
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; }
        public IConfiguration Configuration { get; }
        public Startup(IHostingEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            LoadModules();
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);
            services.AddZNxtSSO(Environment);
            services.AddZNxtApp();
            services.AddZNxtBearerAuthentication();
            services.AddZNxtIdentityServer();
            services.AddTransient<ILogger, ConsoleLogger>();
            services.AddTransient<ILogReader, ConsoleLogger>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var fordwardedHeaderOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };
            fordwardedHeaderOptions.KnownNetworks.Clear();
            fordwardedHeaderOptions.KnownProxies.Clear();

            app.UseForwardedHeaders(fordwardedHeaderOptions);
            var corurl = CommonUtility.GetAppConfigValue("cor_urls");
            if (string.IsNullOrEmpty(corurl))
            {
                corurl = "http://localhost:50071";
            }
            app.UseCors(
                    options => options.WithOrigins(corurl.Split(';')).AllowAnyMethod()
             );
            app.UseZNxtSSO();
            var ssourl = CommonUtility.GetAppConfigValue(CommonConst.CommonValue.SSOURL_CONFIG_KEY);
            if (!string.IsNullOrEmpty(ssourl))
            {
                app.UseAuthentication();
            }
            app.UseZNxtApp();

        }

        private void LoadModules()
        {
            Console.WriteLine("Loading modules .... ");
            Console.WriteLine(EventConsts.GetServiceInfo());
           /* Console.WriteLine(GameApiConsts.GetServiceInfo());
            Console.WriteLine(ProfileConsts.GetServiceInfo());
            Console.WriteLine(GamePlayConsts.GetServiceInfo());
            Console.WriteLine(GameRepoConsts.GetServiceInfo());
            Console.WriteLine(CampaignConsts.GetServiceInfo()); 
            Console.WriteLine(NotifierConsts.GetServiceInfo());
            Console.WriteLine(ContentMgtConsts.GetServiceInfo());
            Console.WriteLine(ContentMgtConsts.GetServiceInfo());
            Console.WriteLine(TenantMgmtConsts.GetServiceInfo());
            Console.WriteLine(SegmentConsts.GetServiceInfo());
           */
        }
    }
}
