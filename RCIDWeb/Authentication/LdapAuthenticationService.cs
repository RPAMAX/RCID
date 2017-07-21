using RCIDWeb.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Web;

namespace RCIDWeb.Authentication
{
    public class LdapAuthenticationService : IAuthenticationService
    {
        private const string Sn = "sn";
        private const string Ou = "ou";
        private const string GivenName = "givenname";
        private const string Email = "mail";
        private const string server = "10.10.10.102";

        private readonly LdapConnection _connection;

        public LdapAuthenticationService()
        {
            _connection = new LdapConnection(new LdapDirectoryIdentifier(server));
            _connection.SessionOptions.SecureSocketLayer = true;
            _connection.SessionOptions.ProtocolVersion = 3;
        }

        public User Login(string username, string password)
        {
            _connection.Bind(new System.Net.NetworkCredential(username + "@auxis.com", password));

            var searchFilter = "samaccountname=" + username;
            var justthese = new[] { Ou, Sn, GivenName, Email };
            var disName = "DC=auxis,DC=com";

            SearchRequest request = new SearchRequest(disName, searchFilter, SearchScope.Subtree, justthese);
            SearchResponse result = (SearchResponse)_connection.SendRequest(request);

            try
            {
                var user = result.Entries[0];
                string dn = user.DistinguishedName;
                if (user != null)
                {
                    _connection.Credential = new System.Net.NetworkCredential(dn, password);
                    _connection.Bind();

                    return new User
                    {
                        GivenName = user.Attributes[GivenName].Name,
                        Sn = user.Attributes[Sn].Name,
                        Email = user.Attributes[Email].Name
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Login failed.");
            }
            //_connection.Disconnect();
            return null;
        }
    }
}