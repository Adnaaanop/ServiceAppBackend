using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Domain.Entities.Catalog
{
    public class ServiceCategory : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public Guid? ParentCategoryId { get; set; }
        public ServiceCategory? ParentCategory { get; set; }

        public ICollection<ServiceCategory> ChildCategories { get; set; } = new List<ServiceCategory>();
        public ICollection<Service> Services { get; set; } = new List<Service>();

        // Optional: Allows tracking category popularity for admin reports
        public int PopularityScore { get; set; } = 0;
    }
}
