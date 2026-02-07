using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
// JWT / IdentityModel
using Microsoft.Owin.Security.Jwt;
// OWIN
using Owin;
using System;
using System.Configuration;
using System.Text;

[assembly: OwinStartup(typeof(MareneStore.Api.App_Start.Startup))]

namespace MareneStore.Api.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var secret = ConfigurationManager.AppSettings["JwtSecret"];
            var issuer = ConfigurationManager.AppSettings["JwtIssuer"];
            var audience = ConfigurationManager.AppSettings["JwtAudience"];
            // API JWT
            var key = Encoding.UTF8.GetBytes(secret);
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }
            });
        }
    }
}
