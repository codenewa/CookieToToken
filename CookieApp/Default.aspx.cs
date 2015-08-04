using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CookieApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            var currLoginUser = txtUserName.Text;

            HttpCookie cookie = FormsAuthentication.GetAuthCookie(currLoginUser, false);
            FormsAuthenticationTicket oldTicket = FormsAuthentication.Decrypt(cookie.Value);
            string credentials = currLoginUser;
            FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(oldTicket.Version, currLoginUser, oldTicket.IssueDate, oldTicket.Expiration, oldTicket.IsPersistent, credentials);
            string encryptedTicket = FormsAuthentication.Encrypt(newTicket);
            cookie.Value = encryptedTicket;
            Response.Cookies.Add(cookie);

            Response.Redirect("SecurePage.aspx");
        }
    }
}