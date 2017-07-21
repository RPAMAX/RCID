using RCIDWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCIDWeb.Authentication
{
    public interface IAuthenticationService
    {
        User Login(string username, string password);
    }
}
