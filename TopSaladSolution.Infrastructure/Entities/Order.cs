using System;
using System.Collections.Generic;
using TopSaladSolution.Common.Enums;

namespace TopSaladSolution.Infrastructure.Entities
{
    public class Order
    {
        public int Id { set; get; }
        public DateTime OrderDate { set; get; }
        public Guid UserId { set; get; }
        public string? RecipientName { set; get; }
        public string? RecipientAddress { set; get; }
        public string? RecipientEmail { set; get; }
        public string? RecipientPhoneNumber { set; get; }
        public ItemStatus Status { set; get; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public AppUser AppUser { get; set; }
    }
}
