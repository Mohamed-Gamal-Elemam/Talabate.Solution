using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabate.Core.Entity.Identity;

namespace Talabate.APIs.Extantions
{
    public static class userMangerExtation
    {
        public static  async Task<ApplicationUser> FindUserWithAddressAsync(this UserManager<ApplicationUser> userManager ,ClaimsPrincipal User)
        {
            var email= User.FindFirstValue(ClaimTypes.Email);
            var user =await userManager.Users.Include(U=>U.Address).SingleOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
