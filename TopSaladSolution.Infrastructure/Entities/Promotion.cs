using System;
using TopSaladSolution.Common.Enums;

namespace TopSaladSolution.Infrastructure.Entities
{
    public class Promotion : BaseEntity
    {
        public string? Name { set; get; }
        public string? Description { set; get; }
        public DateTime FromDate { set; get; }
        public DateTime ToDate { set; get; }
        public bool ApplyForAll { set; get; }
        public int? DiscountPercent { set; get; }
        public decimal? DiscountAmount { set; get; }
        public string ProductIds { set; get; }
        public string ProductCategoryIds { set; get; }
        public ItemStatus Status { set; get; }
    }
}
