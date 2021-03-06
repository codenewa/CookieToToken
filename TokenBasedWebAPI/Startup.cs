﻿using Microsoft.Owin.Cors;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using TokenBasedWebAPI.Provider;
using UI.MiddleWare;

namespace TokenBasedWebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            app.UseCookieParserMiddleWare();
            app.ConfigureSecurity();
            app.ConfigureWebAPI(config);
        }
    }


    public static class AppConfigurationExtensions
    {

        public static void ConfigureSecurity(this IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseOAuthAuthorizationServer(new Microsoft.Owin.Security.OAuth.OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath=new Microsoft.Owin.PathString("/token"),
                AccessTokenExpireTimeSpan=TimeSpan.FromDays(1),
                Provider = new CIRAuthorizationServerProvider()
            });

            app.UseOAuthBearerAuthentication(new Microsoft.Owin.Security.OAuth.OAuthBearerAuthenticationOptions());
        }

        public static void ConfigureWebAPI(this IAppBuilder app, HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "default",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            app.UseWebApi(config);

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}