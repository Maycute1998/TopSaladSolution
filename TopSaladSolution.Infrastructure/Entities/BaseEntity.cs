using System;
using TopSaladSolution.Common.Enums;

namespace TopSaladSolution.Infrastructure.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public ItemStatus Status { get; set; } = ItemStatus.Active;
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
    }
}
