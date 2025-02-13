using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeService.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class AddExpert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Experts",
                columns: new[] { "Id", "Address", "Balance", "Biography", "CityId", "Fname", "ImagePath", "IsConfirmed", "Lname", "UserId" },
                values: new object[] { 1, null, 100000m, null, 1, "Expert", null, false, "experti", 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Experts",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
