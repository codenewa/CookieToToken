using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace TokenBasedWebAPI.Controller
{
    [RoutePrefix("api/test")]
    public class TestController: ApiController
    {

        [HttpGet]
        [Route("hello")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> SayHello() {
            return await Task.FromResult<IHttpActionResult>(Ok("Hello World"));
        }

    }
}