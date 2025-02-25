using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HomeService.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class addClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin", 1 },
                    { 2, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "Admin@gmail.com", 1 },
                    { 3, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Expert", 2 },
                    { 4, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "Expert@gmail.com", 2 },
                    { 5, "ExpertId", "1", 2 },
                    { 6, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Customer", 3 },
                    { 7, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "Customer@gmail.com", 3 },
                    { 8, "CustomerId", "1", 3 }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "LockoutEnabled",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "LockoutEnabled",
                value: true);
        }
    }
}
