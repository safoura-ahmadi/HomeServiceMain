using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HomeService.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class FixName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { "Fname", "Safoura", 1 });

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Expert" });

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "Expert@gmail.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { "Fname", "Tahoura", 2 });

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { "ExpertId", "1", 2 });

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Customer" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 9, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "Customer@gmail.com", 3 },
                    { 10, "CustomerId", "1", 3 },
                    { 11, "Fname", "Mahoura", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Expert", 2 });

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "Expert@gmail.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "ExpertId", "1" });

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Customer", 3 });

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "Customer@gmail.com", 3 });

            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "CustomerId", "1" });
        }
    }
}
