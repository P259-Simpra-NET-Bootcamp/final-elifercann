using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace simApi.Data.Context;

public static class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var roleAdmin = new IdentityRole { Name = "admin", NormalizedName = "ADMIN" };
        var roleUser = new IdentityRole { Name = "user", NormalizedName = "USER" };

        modelBuilder.Entity<IdentityRole>().HasData(roleAdmin, roleUser);

        var adminUser = new AppUser
        {
            UserName = "admin",
            Email = "admin@example.com",
            FirstName = "Admin",
            LastName = "Admin",
            NormalizedEmail = "ADMIN@EXAMPLE.COM",
            NormalizedUserName = "ADMIN",
            EmailConfirmed = true,
            TwoFactorEnabled = false,
            Roles="admin"
        };

        var passwordHasher = new PasswordHasher<AppUser>();
        adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "adminpassword");

        modelBuilder.Entity<AppUser>().HasData(adminUser);

        var userRole = new IdentityUserRole<string>
        {
            RoleId = roleAdmin.Id,
            UserId = adminUser.Id
        };

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRole);
    }
}

