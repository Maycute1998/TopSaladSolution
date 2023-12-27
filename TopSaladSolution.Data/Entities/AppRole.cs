using Microsoft.AspNetCore.Identity;

namespace TopSaladSolution.Infrastructure.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
