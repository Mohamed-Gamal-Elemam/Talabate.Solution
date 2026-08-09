using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Talabate.Core.Entity.Identity;

namespace Talabate.Infrastructer.Identity
{
    public static class AppIdentityDataSeeding
    {
        
        public static async Task SeedData( UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new ApplicationUser()
                {
                    DisplayName = "mohamed Gamal",
                    Email = "MohamedGamal@gmail.com",
                    UserName = "mohmaed_gamal",
                }; 
                await userManager.CreateAsync(User, "Mohamed123");
            }



        }
    }
}
