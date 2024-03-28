using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;

namespace PsychosocialSupportPlatformAPI.DataAccess.DataSeeding
{
    public static class SeedData
    {
        public static void Seeding(ModelBuilder builder)
        {
            ApplicationUser applicationUser = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                Surname = "Admin",
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                NormalizedUserName = "ADMIN",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            PasswordHasher<ApplicationUser> passwordHasher = new();
            applicationUser.PasswordHash = passwordHasher.HashPassword(applicationUser, "Admin*123");

            builder.Entity<ApplicationUser>().HasData(applicationUser);

            ApplicationRole applicationRole = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                ConcurrencyStamp = "1",
                NormalizedName = "ADMIN"
            };

            builder.Entity<ApplicationRole>().HasData(applicationRole);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = applicationRole.Id, UserId = applicationUser.Id }
                );
        }
    }
}
