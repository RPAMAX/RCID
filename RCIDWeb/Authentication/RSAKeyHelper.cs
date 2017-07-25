using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace RCIDWeb.Authentication
{
    public static class RSAKeyHelper
    {
        public static RSA GenerateKey()
        {
            using (var key = new RSACryptoServiceProvider(2048))
            {
                return key;
            }
        }
    }
}