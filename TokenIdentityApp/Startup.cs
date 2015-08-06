using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TokenIdentityApp.MiddleWare;
using TokenIdentityApp.Provider;

namespace TokenIdentityApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseCookieParserMiddleWare();
            ConfigureOAuth(app);

            var config = new HttpConfiguration();
            WebApiConfig.Register(config);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            //var serverOptions = new OAuthAuthorizationServerOptions()
            //{
            //    AuthenticationMode=Microsoft.Owin.Security.AuthenticationMode.Active,
            //    AllowInsecureHttp = true,
            //    TokenEndpointPath = new PathString("/token"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
            //    Provider = new CIRAuthorizationServerProvider()
            //};
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            var implicitGrantServerOptions = new OAuthAuthorizationServerOptions
            {
                AuthorizeEndpointPath= new PathString("/token"),
                Provider= new CIRImplicitAuthorizationServerProvider(),
                AllowInsecureHttp = true,
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(100)
            };

            app.UseOAuthAuthorizationServer(implicitGrantServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                AuthenticationType="Bearer",
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active
            });
        }
    }
}