using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Owin;
using RCIDWeb.Authentication;
using System;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace RCIDWeb
{
    public partial class Startup
    {
        public void ConfigureOAuth(IAppBuilder app)
        {
            var secret = TextEncodings.Base64Url.Decode("IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw");

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = TokenAuthOption.Key,
                        ValidAudience = TokenAuthOption.Audience,
                        ValidIssuer = TokenAuthOption.Issuer,
                        // When receiving a token, check that we've signed it.
                        ValidateIssuerSigningKey = true,
                        // When receiving a token, check that it is still valid.
                        ValidateLifetime = true,
                        // This defines the maximum allowable clock skew - i.e. provides a tolerance on the token expiry time 
                        // when validating the lifetime. As we're creating the tokens locally and validating them on the same 
                        // machines which should have synchronised time, this can be set to zero. Where external tokens are
                        // used, some leeway here could be useful.
                        ClockSkew = TimeSpan.FromMinutes(0)
                    }
                });

        }
    }
}