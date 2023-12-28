using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSaladSolution.Infrastructure.Enums;

namespace TopSaladSolution.Infrastructure.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public Status Status { get; set; } = Status.Active;
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
    }
}
