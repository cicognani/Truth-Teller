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
                        
                    // Use this zone for a complete log
                                        

                        return response;
                
                });
        }
    }
}