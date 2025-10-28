using AutoMapper;
using MyApp_backend.Application.DTOs.Provider;
using MyApp_backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyApp_backend.Application.Mapping
{
    public class ProviderProfileMapping : Profile
    {
        public ProviderProfileMapping()
        {
            // Entity to DTO
            CreateMap<ProviderProfile, ProviderProfileDto>()
                .AfterMap((src, dest) =>
                {
                    dest.ServiceCategories = string.IsNullOrEmpty(src.ServiceCategoriesJson)
                        ? new List<string>()
                        : JsonSerializer.Deserialize<List<string>>(src.ServiceCategoriesJson);
                    dest.CertificateUrls = string.IsNullOrEmpty(src.CertificateUrlsJson)
                        ? new List<string>()
                        : JsonSerializer.Deserialize<List<string>>(src.CertificateUrlsJson);
                    dest.LicenseUrls = string.IsNullOrEmpty(src.LicenseUrlsJson)
                        ? new List<string>()
                        : JsonSerializer.Deserialize<List<string>>(src.LicenseUrlsJson);
                    dest.DocumentUrls = string.IsNullOrEmpty(src.DocumentUrlsJson)
                        ? new List<string>()
                        : JsonSerializer.Deserialize<List<string>>(src.DocumentUrlsJson);
                    dest.ServiceAreas = string.IsNullOrEmpty(src.ServiceAreasJson)
                        ? new List<string>()
                        : JsonSerializer.Deserialize<List<string>>(src.ServiceAreasJson);
                });

            // DTO to Entity
            CreateMap<ProviderProfileDto, ProviderProfile>()
                .AfterMap((src, dest) =>
                {
                    dest.ServiceCategoriesJson = JsonSerializer.Serialize(src.ServiceCategories ?? new List<string>());
                    dest.CertificateUrlsJson = JsonSerializer.Serialize(src.CertificateUrls ?? new List<string>());
                    dest.LicenseUrlsJson = JsonSerializer.Serialize(src.LicenseUrls ?? new List<string>());
                    dest.DocumentUrlsJson = JsonSerializer.Serialize(src.DocumentUrls ?? new List<string>());
                    dest.ServiceAreasJson = JsonSerializer.Serialize(src.ServiceAreas ?? new List<string>());
                });
        }
    }
}
