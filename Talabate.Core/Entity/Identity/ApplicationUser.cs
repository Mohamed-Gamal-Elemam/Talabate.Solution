using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Talabate.Core.Entity.Identity
{
    public class ApplicationUser :IdentityUser
    {
        public string DisplayName { get; set; } = null!;
        public virtual  Address? Address { get; set; }
    }
}
