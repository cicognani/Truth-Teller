using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(KmovieS.Startup))]
namespace KmovieS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
    
            ConfigureAuth(app);
 
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
     

        }
        public void ConfigureOAuth(IAppBuilder app)
        {

           
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = false,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
               // Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

  
        }
    }
}
