using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Owin.Security.OAuth;
using OWINCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OWINCore.Common
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {


        private readonly UserManagerUtil _appUserManager1;

        public ApplicationOAuthProvider()
        {

        }
        public ApplicationOAuthProvider(UserManagerUtil appUserManager1)
        {
            _appUserManager1 = appUserManager1;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
           
            var user = await _appUserManager1.FindByNameAsync(context.UserName);
            if (user != null)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("Username", user.UserName));
                identity.AddClaim(new Claim("Email", user.Email));
                identity.AddClaim(new Claim("FirstName", user.FirstName));
                identity.AddClaim(new Claim("LastName", user.LastName));
                identity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString()));
                context.Validated(identity);
            }
            else
                return;
        }
    }
}
