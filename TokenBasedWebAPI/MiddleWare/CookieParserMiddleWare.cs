using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace UI.MiddleWare
{
    public class CookieParserMiddleWare
    {
        private Func<IDictionary<string, object>, Task> _next;

        public CookieParserMiddleWare(Func<IDictionary<string,object>, Task> next){
            _next = next;         
        }

        public async Task Invoke(IDictionary<string,object> env)
        {
            var context = new OwinContext(env);

            var authCookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            var authTicket = FormsAuthentication.Decrypt(authCookie);

            context.Request.User = new GenericPrincipal(new GenericIdentity(authTicket.UserData), new String[] { });

            await _next(env);
        }
    }

    public static class AppExtensions
    {
        public static void UseCookieParserMiddleWare(this IAppBuilder app)
        {
            app.Use(typeof(CookieParserMiddleWare));
        }
    }
}