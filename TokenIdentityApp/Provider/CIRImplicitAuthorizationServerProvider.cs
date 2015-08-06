using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using System.Web.Security;

namespace TokenIdentityApp.Provider
{
    class CIRImplicitAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            var authCookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            var authTicket = FormsAuthentication.Decrypt(authCookie);

            if (authTicket.Expired)
                context.Rejected();
            else
                context.Validated();

            //We validated that Client Id and Reditect Uri are indeed what we expect
            //if (context.ClientId == "123456" && context.RedirectUri.Contains("localhost"))
            //    context.Validated();
            //else
            //    context.Rejected();

            return Task.FromResult<object>(null);
        }

        public override Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context)
        {
            // The authentication types should be set to "Bearer" while setting up the ClaimsIdentity
            // I have set up basic mandatory ClaimsIdentity. You can add the necessary claims if required.
            System.Security.Claims.ClaimsIdentity ci = new System.Security.Claims.ClaimsIdentity("Bearer");
            context.OwinContext.Authentication.SignIn(ci);
            context.RequestCompleted();
            return Task.FromResult<object>(null);
        }

    }
}