using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace KmovieS.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values 
        [Authorize(Roles = "Admin")]
        public IEnumerable<string> Get()
        {
            var httpRequest = HttpContext.Current.Request;
            if (!httpRequest.IsAuthenticated)
            {
                return new string[] { "Not Authorized" };
            }
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [Authorize]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [Authorize]
        public HttpResponseMessage Post([FromBody]string value)
        {
            var httpRequest = HttpContext.Current.Request;
            if (!httpRequest.IsAuthenticated)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            return Request.CreateResponse(HttpStatusCode.Created, value);
        }

        // PUT api/values/5
        [Authorize]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [Authorize]
        public void Delete(int id)
        {
        }
    }
}
