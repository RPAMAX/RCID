using Microsoft.AspNet.Identity.Owin;

namespace RCIDWeb.Authentication
{
    public interface IAuthenticationService
    {
        SignInStatus PasswordSignIn(string username, string password);
    }
}
