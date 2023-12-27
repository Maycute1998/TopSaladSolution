namespace TopSaladSolution.Infrastructure.Entities
{
    public class Slide : BaseEntity
    {
        public string? Name { set; get; }
        public string? Description { set; get; }
        public string? Url { set; get; }

        public string? Image { get; set; }
        public int SortOrder { get; set; }
    }
}
