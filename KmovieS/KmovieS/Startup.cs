﻿using AspNetIdentity.WebApi.Providers;
using KmovieS.Models;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Configuration;
using System.Web.Http;
using System.Net.Http.Formatting;

[assembly: OwinStartupAttribute(typeof(KmovieS.Startup))]
namespace KmovieS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
    
            ConfigureOAuthTokenGeneration(app);
            ConfigureOAuthTokenConsumption(app);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
     

        }
        public void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Configurare il contesto di database, la gestione utenti e la gestione accessi in modo da usare un'unica istanza per richiesta
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);


            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = false,
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat("http://localhost:44386")
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

        }

        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {

            var issuer = "http://localhost:44386";
            string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceId },
                    IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[]
                    {
                        new SymmetricKeyIssuerSecurityKeyProvider(issuer, audienceSecret)
                    }
                });
        }
    }
}
