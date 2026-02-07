using System;
using System.Text;

// OWIN
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

// JWT / IdentityModel
using Microsoft.Owin.Security.Jwt;
using Microsoft.IdentityModel.Tokens;

[assembly: OwinStartup(typeof(MareneStore.Api.App_Start.Startup))]

namespace MareneStore.Api.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // API JWT
            var key = Encoding.UTF8.GetBytes("uma-chave-muito-secreta-aqui");
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "MareneStoreAPI",
                    ValidAudience = "MareneStoreClient",
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }
            });
        }
    }
}
