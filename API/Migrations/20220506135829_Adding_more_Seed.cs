using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class Adding_more_Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TItem",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 4, "Mhh tasty...", "Stomb's fries" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TItem",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
