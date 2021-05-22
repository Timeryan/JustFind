using Advertisement.Application.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IdentityUser = Advertisement.Infrastructure.Identity.IdentityUser;

namespace Advertisement.Infrastructure.DataAccess.RepositoryInDataBase.EntityConfiguration
{
    public class UserIdentityConfiguration
    {
        public static void SeedIdentity(ModelBuilder modelBuilder)
        {
            var ADMIN_ROLE_ID = "d3300ca5-846f-4e6b-ac5f-1d3933115e67";
            var ADMIN_ID = "98b651ae-c9aa-4731-9996-57352d525f7e";
            var USER_ROLE_ID = "185230d2-58d8-4e29-aefd-a257fb82a150";

            modelBuilder.Entity<IdentityRole>(x =>
            {
                x.HasData(new[]
                {
                    new IdentityRole
                    {
                        Id = ADMIN_ROLE_ID,
                        Name = RoleConstants.AdminRole,
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole
                    {
                        Id = USER_ROLE_ID,
                        Name = RoleConstants.UserRole,
                        NormalizedName = "USER"
                    }
                });
            });

            var passwordHasher = new PasswordHasher<IdentityUser>();
            var adminUser = new IdentityUser
            {
                Id = ADMIN_ID,
                UserName = "admin",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                NormalizedUserName = "ADMIN",
                EmailConfirmed = true
            };
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "admin");

            modelBuilder.Entity<IdentityUser>(x =>
            {
                x.HasData(adminUser);
            });

            modelBuilder.Entity<IdentityUserRole<string>>(x =>
            {
                x.HasData(new IdentityUserRole<string>
                {
                    RoleId = ADMIN_ROLE_ID,
                    UserId = ADMIN_ID
                });
            });
        }
    }
}