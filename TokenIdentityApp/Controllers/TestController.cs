using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace TokenIdentityApp.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController: ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("hello")]
        public async Task<IHttpActionResult> SayHello() {
            return await Task.FromResult<IHttpActionResult>(Ok("Hello World : " + User.Identity.Name));
        }

    }
}