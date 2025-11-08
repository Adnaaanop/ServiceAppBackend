using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Domain.Entities.WarrantySafety
{
    public class WarrantyRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? ProviderId { get; set; }
        public string ProductName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime RequestDate { get; set; }
        public WarrantyStatus Status { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Description { get; set; }
    }

    public enum WarrantyStatus
    {
        Pending,
        Approved,
        Rejected,
        Completed
    }
}
