using Microsoft.AspNet.Identity.Owin;
using RCIDWeb.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Web;

namespace RCIDWeb.Authentication
{
    public class LdapAuthenticationService : IAuthenticationService
    {
        private const string server = "10.10.10.102";

        public SignInStatus PasswordSignIn(string userName, string password)
        {
            var user = Login(userName, password);

            if(user != null)
            {
                return SignInStatus.Success;
            }
            return SignInStatus.Failure;
        }

        private UserPrincipal Login(string username, string password)
        {
            var domain = "auxis";
            var container = "OU=Users,OU=Organization,DC=auxis,DC=com";

            try
            {
                PrincipalContext ctx = new PrincipalContext(ContextType.Domain, domain, container);

                if(ctx.ValidateCredentials(username, password))
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(ctx, username);

                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Login failed.");
            }
        }
    }
}