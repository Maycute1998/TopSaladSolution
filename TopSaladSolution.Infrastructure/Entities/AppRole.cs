using Microsoft.AspNetCore.Identity;
using System;

namespace TopSaladSolution.Infrastructure.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
