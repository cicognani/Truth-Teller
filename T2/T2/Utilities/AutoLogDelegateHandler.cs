using T2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace T2.Utilities
{
    public class AutoLogDelegateHandler : DelegatingHandler
    {

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestBody = request.Content.ReadAsStringAsync().Result;

            return await base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {

                   
                    HttpResponseMessage response = task.Result;
                        
                    // Si può utilizzare questa zona per creare un log completo delle chiamata
                        //var Req = request.ToString();
                        //int firstapex = Req.IndexOf("'");
                        //int secondapex = Req.IndexOf("'", firstapex + 1);
                        //int lastslash = Req.Substring(0, secondapex).LastIndexOf("/");
                        //string myAPIFullName = Req.Substring(lastslash + 1, secondapex - lastslash - 1);                    

                        return response;
                
                });
        }
    }
}