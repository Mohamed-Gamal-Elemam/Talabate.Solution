using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Talabate.Core.Entity.Identity;

namespace Talabate.Core.IServices
{
    public interface IAuthServices
    {
        Task <string> CreateTokenAsync(ApplicationUser user ,UserManager<ApplicationUser> userManager);
    }
}
