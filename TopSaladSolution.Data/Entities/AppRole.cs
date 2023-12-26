using Microsoft.AspNetCore.Identity;

namespace TopSaladSolution.Data.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
