using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OWINCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OWINCore
{
    public class DatabaseInitializer
    {

        public static void Init(IServiceProvider provider, bool useInMemoryStores)
        {
            if (!useInMemoryStores)
            {
                provider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
                //provider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                //provider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();
            }
            //InitializeIdentityServer(provider);

            //var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
            //var chsakell = userManager.FindByNameAsync("chsakell").Result;
            //if (chsakell == null)
            //{
            //    chsakell = new IdentityUser
            //    {
            //        UserName = "chsakell"
            //    };
            //    var result = userManager.CreateAsync(chsakell, "$AspNetIdentity10$").Result;
            //    if (!result.Succeeded)
            //    {
            //        throw new Exception(result.Errors.First().Description);
            //    }

            //    chsakell = userManager.FindByNameAsync("chsakell").Result;

            //    result = userManager.AddClaimsAsync(chsakell, new Claim[]{
            //        new Claim(JwtClaimTypes.Name, "Chris Sakellarios"),
            //        new Claim(JwtClaimTypes.GivenName, "Christos"),
            //        new Claim(JwtClaimTypes.FamilyName, "Sakellarios"),
            //        new Claim(JwtClaimTypes.Email, "chsakellsblog@blog.com"),
            //        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
            //        new Claim(JwtClaimTypes.WebSite, "https://chsakell.com"),
            //        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'localhost 10', 'postal_code': 11146, 'country': 'Greece' }",
            //            IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
            //    }).Result;

            //    if (!result.Succeeded)
            //    {
            //        throw new Exception(result.Errors.First().Description);
            //    }
            //    Console.WriteLine("chsakell created");
        }
    }
}


