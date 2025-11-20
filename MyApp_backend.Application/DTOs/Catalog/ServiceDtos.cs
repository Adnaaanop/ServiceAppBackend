using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp_backend.Application.DTOs.Catalog
{
    public class ServiceCreateDto
    {
        public Guid ProviderId { get; set; }
        public Guid CategoryId { get; set; }
        public string? Description { get; set; }
        public string PricingJson { get; set; }
        public string? AvailabilityScheduleJson { get; set; }
        public string? MediaUrlsJson { get; set; }
        public List<IFormFile>? MediaFiles { get; set; }
    }

    public class ServiceUpdateDto
    {
        public Guid CategoryId { get; set; }
        public string? Description { get; set; }
        public string PricingJson { get; set; }
        public string? AvailabilityScheduleJson { get; set; }
        public string? MediaUrlsJson { get; set; }
        public bool IsApproved { get; set; }
        public List<IFormFile>? MediaFiles { get; set; }
    }

    public class ServiceResponseDto
    {
        public Guid Id { get; set; }
        public Guid ProviderId { get; set; }
        public Guid CategoryId { get; set; }
        public string? Description { get; set; }
        public string PricingJson { get; set; }
        public string? AvailabilityScheduleJson { get; set; }
        public string? MediaUrlsJson { get; set; }
        public bool IsApproved { get; set; }
        public int TotalBookings { get; set; }
        public double AverageRating { get; set; }
        public DateTime CreatedAt { get; set; }

        // Add this property to indicate soft delete status
        public bool IsDeleted { get; set; }
    }
}
