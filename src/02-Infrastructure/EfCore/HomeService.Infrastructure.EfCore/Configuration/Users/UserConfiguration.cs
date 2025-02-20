using HomeService.Domain.Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserConfiguration
{
    public static void SeedUsers(ModelBuilder builder)
    {
        var users = new List<User>
        {
            new()
            {
                Id = 1,
                Fname = "Safoura",
                Lname = "ahmadi",
                Balance = 100000,
               Status = HomeService.Domain.Core.Enums.Users.UserStatusEnum.Accepted,
                CityId  = 1,
                UserName = "Admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "Admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                PasswordHash = "AQAAAAIAAYagAAAAEEYElkmU8CUuRLrvzHwkr+KZQSIYjReSWOd2FzOAPi3sm+ZxaBpA9F+fqwsJ3soaSA==",
                SecurityStamp = "MS23CZ5FYG75543TAIMC5DDNKCVV7B74",
                ConcurrencyStamp = "d9b0f212-3c43-4c67-bba1-303cc82f13f5",
                LockoutEnd = null,
                LockoutEnabled = false,


            },new()
            {
                Id = 2,
                Fname = "Tahoura",
                Lname = "ahmadi",
                Balance = 100000,
                 Status = HomeService.Domain.Core.Enums.Users.UserStatusEnum.Accepted,
                CityId  = 1,
                UserName = "Expert@gmail.com",
                NormalizedUserName = "EXPERT@GMAIL.COM",
                Email = "Expert@gmail.com",
                NormalizedEmail = "EXPERT@GMAIL.COM",
                PasswordHash = "AQAAAAIAAYagAAAAEJkglU8KXnLbrWvBHwkr+KZQSIYjReSWOd2FzOAPi3sm+ZxaBpA9F+fqwsJ3soaSA==",
                SecurityStamp = "JK98SD2FYG75543TAIMC5DDNKCVV7B89",
                ConcurrencyStamp = "a3d5f1c2-9b12-4e7a-a3c1-45edc91e36b7",
                LockoutEnd = null,
                LockoutEnabled = false,
            },

            new()
            {
                Id = 3,
                Fname = "Mahoura",
                Lname = "ahmadi",
                Balance = 100000,
                Status = HomeService.Domain.Core.Enums.Users.UserStatusEnum.Accepted,
                CityId  = 1,
                UserName = "Customer@gmail.com",
                NormalizedUserName = "CUSTOMER@GMAIL.COM",
                Email = "Customer@gmail.com",
                NormalizedEmail = "Customer@GMAIL.COM",
                PasswordHash = "AQAAAAIAAYagAAAAEMGklU8NXYfKrZHvBHwkr+KZQSIYjReSWOd2FzOAPi3sm+ZxaBpA9F+fqwsJ3soaSA==",
                SecurityStamp = "PQ76XZ9FYG75543TAIMC5DDNKCVV7B32",
                ConcurrencyStamp = "f7c8e9a1-2b34-4d59-931a-72bf4c61c5f9",
                LockoutEnd = null,
                LockoutEnabled = false,
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
             new IdentityUserRole<int>() { RoleId = 1, UserId = 1 },
                  new IdentityUserRole<int>() { RoleId = 2, UserId = 2 },
                       new IdentityUserRole<int>() { RoleId = 3, UserId = 3 }
        );
    }
}