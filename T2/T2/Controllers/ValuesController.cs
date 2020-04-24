using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace T2.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values per ottenere lo UserID
        [Authorize(Roles = "User")]
        public IEnumerable<string> Get()
        {
            var httpRequest = HttpContext.Current.Request;
            if (!httpRequest.IsAuthenticated)
            {
                return new string[] { "Not Authorized" };
            }
            return new string[] { User.Identity.GetUserId().ToString()};
        }
       
    }
}
