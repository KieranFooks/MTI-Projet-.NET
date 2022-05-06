using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class AddingTinventories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TInventory",
                columns: new[] { "Id_item", "Id_user", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 1 },
                    { 3, 2, 5 }
                });

            migrationBuilder.UpdateData(
                table: "TMarket",
                keyColumn: "Id",
                keyValue: 1,
                column: "Quantity",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TMarket",
                keyColumn: "Id",
                keyValue: 2,
                column: "Quantity",
                value: 1);

            migrationBuilder.InsertData(
                table: "TMarket",
                columns: new[] { "Id", "Id_item", "Id_seller", "Is_sold", "Price", "Quantity" },
                values: new object[,]
                {
                    { 3, 2, 1, true, 0, 1 },
                    { 4, 2, 2, false, 0, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TInventory",
                keyColumns: new[] { "Id_item", "Id_user" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "TInventory",
                keyColumns: new[] { "Id_item", "Id_user" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "TInventory",
                keyColumns: new[] { "Id_item", "Id_user" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "TMarket",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TMarket",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "TMarket",
                keyColumn: "Id",
                keyValue: 1,
                column: "Quantity",
                value: 0);

            migrationBuilder.UpdateData(
                table: "TMarket",
                keyColumn: "Id",
                keyValue: 2,
                column: "Quantity",
                value: 0);
        }
    }
}
