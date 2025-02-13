using HomeService.Domain.Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserConfiguration
{
    public static void SeedUsers(ModelBuilder builder)
    {
        var users = new List<User>
        {
            new User()
            {
                Id = 1,
                UserName = "Admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "Admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                LockoutEnabled = false,
                                PasswordHash = "AQAAAAIAAYagAAAAEEYElkmU8CUuRLrvzHwkr+KZQSIYjReSWOd2FzOAPi3sm+ZxaBpA9F+fqwsJ3soaSA==",
                 SecurityStamp = "MS23CZ5FYG75543TAIMC5DDNKCVV7B74"


            }

        };
        builder.Entity<User>().HasData(users);




        // Seed Roles
        builder.Entity<IdentityRole<int>>().HasData(
            new IdentityRole<int>() { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole<int>() { Id = 2, Name = "Expert", NormalizedName = "EXPERT" },
            new IdentityRole<int>() { Id = 3, Name = "Customer", NormalizedName = "CUSTOMER" }
        );

        //// Seed Role To Users
        builder.Entity<IdentityUserRole<int>>().HasData(
             new IdentityUserRole<int>() { RoleId = 1, UserId = 1 }
        );
    }
}