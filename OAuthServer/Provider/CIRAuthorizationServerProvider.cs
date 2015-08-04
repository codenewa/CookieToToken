using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace OAuthServer.Provider
{

    //OnValidateClientRedirectUri = ValidateClientRedirectUri,
    //OnValidateClientAuthentication = ValidateClientAuthentication,
    //OnGrantResourceOwnerCredentials = GrantResourceOwnerCredentials,
    //OnGrantClientCredentials = GrantClientCredetails
    public class CIRAuthorizationServerProvider: OAuthAuthorizationServerProvider
    {
        // This is if client sends credentials. Do we take on the cookie here? Probably not.
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        //Do the credential Validation here. 
        // Also setup the claims here.
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            
            //await base.GrantResourceOwnerCredentials(context);
        }

        public override async  Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            await base.GrantClientCredentials(context);
        }

        public override async Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            await base.ValidateClientRedirectUri(context);
        }
    }
}