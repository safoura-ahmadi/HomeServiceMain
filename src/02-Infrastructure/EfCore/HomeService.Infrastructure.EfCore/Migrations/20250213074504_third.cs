using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HomeService.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 2, 0, "a3d5f1c2-9b12-4e7a-a3c1-45edc91e36b7", "Expert@gmail.com", false, true, null, "EXPERT@GMAIL.COM", "EXPERT@GMAIL.COM", "AQAAAAIAAYagAAAAEJkglU8KXnLbrWvBHwkr+KZQSIYjReSWOd2FzOAPi3sm+ZxaBpA9F+fqwsJ3soaSA==", null, false, "JK98SD2FYG75543TAIMC5DDNKCVV7B89", false, "Expert@gmail.com" },
                    { 3, 0, "f7c8e9a1-2b34-4d59-931a-72bf4c61c5f9", "Customer@gmail.com", false, false, null, "Customer@GMAIL.COM", "CUSTOMER@GMAIL.COM", "AQAAAAIAAYagAAAAEMGklU8NXYfKrZHvBHwkr+KZQSIYjReSWOd2FzOAPi3sm+ZxaBpA9F+fqwsJ3soaSA==", null, false, "PQ76XZ9FYG75543TAIMC5DDNKCVV7B32", false, "Customer@gmail.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
