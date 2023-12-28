using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSaladSolution.Infrastructure.Enums;

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
        public OrderStatus Status { set; get; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public AppUser AppUser { get; set; }
    }
}
