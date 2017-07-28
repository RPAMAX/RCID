using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.DirectoryServices.AccountManagement;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace RCIDWeb.Authentication
{
    public class AdAuthenticationService: IAuthenticationService
    {
        private IAuthenticationManager authenticationManager;

        public AdAuthenticationService(IAuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
        }

        public SignInStatus PasswordSignIn(string username, string password)
        {
            ContextType authenticationType = ContextType.Domain;
            SignInStatus result = SignInStatus.Failure;

            PrincipalContext principalContext = new PrincipalContext(authenticationType);
            bool isAuthenticated = false;
            UserPrincipal userPrincipal = null;

            try
            {
                isAuthenticated = principalContext.ValidateCredentials(username, password, ContextOptions.Negotiate);

                if(isAuthenticated)
                {
                    result = SignInStatus.Success;

                    if (isAuthenticated)
                    {
                        userPrincipal = UserPrincipal.FindByIdentity(principalContext, username);
                    }
                }
            }
            catch (Exception)
            {
                isAuthenticated = false;
                result = SignInStatus.Failure;
            }

            if (!isAuthenticated || userPrincipal == null)
            {
                result = SignInStatus.RequiresVerification;
            }

            if (userPrincipal.IsAccountLockedOut())
            {
                result = SignInStatus.LockedOut;
            }

            var identity = CreateIdentity(userPrincipal);

            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);

            return result;
        }

        private ClaimsIdentity CreateIdentity(UserPrincipal userPrincipal)
        {
            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "Active Directory"));
            identity.AddClaim(new Claim(ClaimTypes.Name, userPrincipal.SamAccountName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userPrincipal.SamAccountName));
            if (!String.IsNullOrEmpty(userPrincipal.EmailAddress))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, userPrincipal.EmailAddress));
            }

            // add your own claims if you need to add more information stored on the cookie

            return identity;
        }
    }
}