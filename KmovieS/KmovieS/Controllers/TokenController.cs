using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using System.Net.Http.Headers;
using System.Text;

namespace KmovieS.Controllers
{
    public class TokenController : ApiController
    {
        private static Tuple<string, string> ExtractUserNameAndPassword(string authorizationParameter)
        {
            byte[] credentialBytes;

            try
            {
                credentialBytes = Convert.FromBase64String(authorizationParameter);
            }
            catch (FormatException)
            {
                return null;
            }

            // The currently approved HTTP 1.1 specification says characters here are ISO-8859-1.
            // However, the current draft updated specification for HTTP 1.1 indicates this encoding is infrequently
            // used in practice and defines behavior only for ASCII.
            Encoding encoding = Encoding.ASCII;
            // Make a writable copy of the encoding to enable setting a decoder fallback.
            encoding = (Encoding)encoding.Clone();
            // Fail on invalid bytes rather than silently replacing and continuing.
            encoding.DecoderFallback = DecoderFallback.ExceptionFallback;
            string decodedCredentials;

            try
            {
                decodedCredentials = encoding.GetString(credentialBytes);
            }
            catch (DecoderFallbackException)
            {
                return null;
            }

            if (String.IsNullOrEmpty(decodedCredentials))
            {
                return null;
            }

            int colonIndex = decodedCredentials.IndexOf(':');

            if (colonIndex == -1)
            {
                return null;
            }

            string userName = decodedCredentials.Substring(0, colonIndex);
            string password = decodedCredentials.Substring(colonIndex + 1);
            return new Tuple<string, string>(userName, password);
        }

        // GET: api/Token
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/Token
        public HttpResponseMessage Post()
        {
            if (!HttpContext.Current.Request.IsAuthenticated) 
            {
                return Request.CreateResponse(HttpStatusCode.Created);
            }

            // Look for credentials in the request.
            AuthenticationHeaderValue authorization = Request.Headers.Authorization;

            // If there are no credentials, do nothing.
            if (authorization == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            // If there are credentials but the filter does not recognize the authentication scheme, do nothing.
            if (authorization.Scheme != "Basic")
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            // 5. If the credentials are bad, set the error result.
            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            Tuple<string, string> userNameAndPassword = ExtractUserNameAndPassword(authorization.Parameter);
            if (userNameAndPassword == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Find(userNameAndPassword.Item1, userNameAndPassword.Item2);

            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            if (authenticationManager.User.Identity.IsAuthenticated)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
            
            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
