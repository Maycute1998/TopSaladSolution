using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace TopSaladSolution.Infrastructure.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public ICollection<Cart> Carts { get; set; } = new List<Cart>();

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
