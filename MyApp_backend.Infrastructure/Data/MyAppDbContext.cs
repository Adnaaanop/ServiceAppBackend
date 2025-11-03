using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyApp_backend.Domain.Entities;
using MyApp_backend.Domain.Entities.Catalog;
using MyApp_backend.Domain.Entities.Payment;
using System;
using System.Reflection.Emit;

namespace MyApp_backend.Infrastructure.Data
{
    public class MyAppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options) { }
        public DbSet<ProviderProfile> ProviderProfiles { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        //Payment
        public DbSet<PricingRule> PricingRules { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Payment> Payments { get; set; }


        //Messaging

        public DbSet<Message> Messages { get; set; }


        public DbSet<UserOtp> UserOtps { get; set; }






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



            // ServiceCategory: Self-referencing parent-child for category hierarchy
            builder.Entity<ServiceCategory>()
                .HasMany(c => c.ChildCategories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // ServiceCategory: Set length constraints and required fields
            builder.Entity<ServiceCategory>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Service: Link to Provider (ApplicationUser)
            builder.Entity<Service>()
                .HasOne(s => s.Provider)
                .WithMany() // Or .WithMany("Services") if you add a collection property to ApplicationUser
                .HasForeignKey(s => s.ProviderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Service: Link to ServiceCategory
            builder.Entity<Service>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Optional: Set length or required constraints for Service properties
            builder.Entity<Service>()
                .Property(s => s.PricingJson)
                .IsRequired();


            //Messages

            builder.Entity<Message>()
                .HasOne(m => m.Booking)
                .WithMany(b => b.Messages)
                .HasForeignKey(m => m.BookingId);

            builder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);



            //UserOtp


            builder.Entity<ApplicationUser>()
                .HasOne(u => u.UserOtp)
                .WithOne(o => o.User)
                .HasForeignKey<UserOtp>(o => o.UserId);



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
