using Microsoft.EntityFrameworkCore.Migrations;

namespace ShelterAPI.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserModels",
                columns: new[] { "UserModelId", "Password", "Username" },
                values: new object[] { 1, "cat123dog", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserModels",
                keyColumn: "UserModelId",
                keyValue: 1);
        }
    }
}
