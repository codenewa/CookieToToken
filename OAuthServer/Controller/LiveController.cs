using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace OAuthServer.Controller
{
    [RoutePrefix("auth/check")]
    public class LiveController : ApiController
    {
        [HttpGet]
        [Route("isAlive")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> GetIsAlive()
        {
            return await Task.FromResult<IHttpActionResult>(Ok("I am alive."));
        }
    }
}