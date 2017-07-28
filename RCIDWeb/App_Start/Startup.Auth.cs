using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
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
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider(),
                ExpireTimeSpan = TimeSpan.FromMinutes(30)
            });
        }
    }
}