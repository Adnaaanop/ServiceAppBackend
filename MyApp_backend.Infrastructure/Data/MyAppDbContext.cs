using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyApp_backend.Domain.Entities;
using System;

namespace MyApp_backend.Infrastructure.Data
{
    public class MyAppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options) { }
        public DbSet<ProviderProfile> ProviderProfiles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole<Guid>>().ToTable("Roles");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");


            // Configure 1:1 User-ProviderProfile relationship
            builder.Entity<ProviderProfile>()
                .HasOne(p => p.User)
                .WithOne(u => u.ProviderProfile)
                .HasForeignKey<ProviderProfile>(p => p.UserId);


            //SEEDING
            // Sample provider user seed
            var providerUserId = Guid.NewGuid();
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var sampleProvider = new ApplicationUser
            {
                Id = providerUserId,
                UserName = "provider@example.com",
                Email = "provider@example.com",
                Name = "John Doe",
                PhoneNumber = "+1234567890",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                IsVerified = true,
                IsActive = true,
                // Password: "Provider@123"
            };
            sampleProvider.PasswordHash = passwordHasher.HashPassword(sampleProvider, "Provider@123");

            builder.Entity<ApplicationUser>().HasData(sampleProvider);

            // Sample provider profile seed
            builder.Entity<ProviderProfile>().HasData(new ProviderProfile
            {
                Id = Guid.NewGuid(),
                UserId = providerUserId,
                BusinessName = "John's Home Services",
                BusinessDescription = "We provide cleaning and handyman services.",
                ServiceCategoriesJson = "[\"Cleaning\",\"General Handyman\"]",
                CertificateUrlsJson = "[]",
                LicenseUrlsJson = "[]",
                DocumentUrlsJson = "[]",
                ServiceAreasJson = "[\"New York, NY\"]",
                AvailabilityJson = "{\"monday\": [\"9-5\"]}",
                IsApproved = true,
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            });
        }
    }
}
