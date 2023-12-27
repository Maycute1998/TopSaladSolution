using TopSaladSolution.Data.Enums;

namespace TopSaladSolution.Data.Entities
{
    public class Contact : BaseEntity
    {
        public string? Name { set; get; }
        public string? Email { set; get; }
        public string? PhoneNumber { set; get; }
        public string? Message { set; get; }
    }
}
