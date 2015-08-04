using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using OAuthServer.Provider;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using Microsoft.Owin.Cors;

namespace OAuthServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.ConfigureSecurity();
        }
    }

    public static class AppConfigurations
    {
        public static void ConfigureSecurity(this IAppBuilder app)
        {
            app.UseExternalSignInCookie();
        }

        public static void ConfigureAuthorizationServer(this IAppBuilder app)
        {
            app.UseOAuthAuthorizationServer(new Microsoft.Owin.Security.OAuth.OAuthAuthorizationServerOptions
            {
                AuthorizeEndpointPath = new PathString(Paths.AuthorizePath),
                TokenEndpointPath = new PathString(Paths.TokenPath),
                AllowInsecureHttp = true,
                // Authorization server provider which controls the lifecycle of Authorization Server
                Provider = new CIRAuthorizationServerProvider(),

                //// Authorization code provider which creates and receives the authorization code.
                AuthorizationCodeProvider = new CIRAuthenticationTokenProvider(),

                //// Refresh token provider which creates and receives refresh token.
                RefreshTokenProvider = new CIRAuthenticationTokenProvider()
            });
        }

        public static void ConfigureWebAPI(this IAppBuilder app)
        {
            var config = new HttpConfiguration();
            app.UseCors(CorsOptions.AllowAll);

            config.Routes.MapHttpRoute(
                name: "default", 
                routeTemplate: "auth/{controller}/{id}", 
                defaults: new { id = RouteParameter.Optional });

            app.UseWebApi(config);
        }
    }

    public static class Paths
    {
        /// <summary>
        /// AuthorizationServer project should run on this URL
        /// </summary>
        public const string AuthorizationServerBaseAddress = "http://localhost:18924";

        /// <summary>
        /// ResourceServer project should run on this URL
        /// </summary>
        public const string ResourceServerBaseAddress = "http://localhost:16723";

        ///// <summary>
        ///// ImplicitGrant project should be running on this specific port '38515'
        ///// </summary>
        //public const string ImplicitGrantCallBackPath = "http://localhost:38515/Home/SignIn";

        ///// <summary>
        ///// AuthorizationCodeGrant project should be running on this URL.
        ///// </summary>
        //public const string AuthorizeCodeCallBackPath = "http://localhost:38500/";

        public const string AuthorizePath = "/OAuth/Authorize";
        public const string TokenPath = "/OAuth/Token";
        public const string LoginPath = "/Account/Login";
        public const string LogoutPath = "/Account/Logout";
        public const string MePath = "/api/Me";
    }
}