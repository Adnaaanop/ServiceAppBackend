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
        }
    }
}
