﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(API.Startup))]

namespace API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            //app.Use(async (context, next) => {
            //    await next();

            //    if (context.Response.StatusCode == 404 && !System.IO.Path.HasExtension(context.Request.Path.Value))
            //    {
            //        context.Request.Path. = new PathString("/index.html"); // Put your Angular root page here 
            //        await next();
            //    }
            //});

            //Register Syncfusion license
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzYwM0AzMTM2MmUzMjJlMzBJS1NsWE4wbXhPQ0dNWG1DanlLdWxuNUlmcm9Qb2w4WjRaZFd0TmxTa3JZPQ==;NzYwNEAzMTM2MmUzMjJlMzBYeS9QTHhlc2lTMkMwN09qSTJDU3p4QXVzeDUxV0ZENFpwSkdzQVhvOGhRPQ==;NzYwNUAzMTM2MmUzMjJlMzBDSEo2TTZKRGljYStxMXh4cHFycGNiL24yOUZwUy9zcUlod2d5eVFVakVnPQ==;NzYwNkAzMTM2MmUzMjJlMzBXeXN0ajZXNytBZ1Vrdlgxa1ZsM2k0aHlmZlI1bG40V005dmx3QlluQUtBPQ==;NzYwN0AzMTM2MmUzMjJlMzBqVTczb0FWRUhmblhtRXRjUnlhbjh5NjZBMUZGaXdYRWNJN2JONnJGb1FZPQ==");

            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureAuth(app);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes); //MVC
            //app.MapSignalR();

            
            
            log4net.Config.XmlConfigurator.Configure();
            log4net.GlobalContext.Properties["appname"] = "DMS-"+ System.Net.Dns.GetHostName();
            
        }
    }
}
