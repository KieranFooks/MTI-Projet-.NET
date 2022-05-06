using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class AddSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TItem",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Used for communication and to see events, whether past or future.", "Palantír" },
                    { 2, "Luminescent plasma blade, can cut steel.", "Lightsaber" },
                    { 3, "Use it to transport a small amount of fluid, like coffee.", "Mug" }
                });

            migrationBuilder.InsertData(
                table: "TUser",
                columns: new[] { "Id", "Money", "Name", "Password" },
                values: new object[,]
                {
                    { 1, 5000, "Gabriel", "test" },
                    { 2, 5000, "Hugo", "test" },
                    { 3, 5000, "Kieran", "test" },
                    { 4, 5000, "Eliott", "test" }
                });

            migrationBuilder.InsertData(
                table: "TMarket",
                columns: new[] { "Id", "Id_item", "Id_seller", "Is_sold", "Price", "Quantity" },
                values: new object[] { 1, 1, 1, false, 0, 0 });

            migrationBuilder.InsertData(
                table: "TMarket",
                columns: new[] { "Id", "Id_item", "Id_seller", "Is_sold", "Price", "Quantity" },
                values: new object[] { 2, 2, 1, false, 0, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_TInventory_Id_item",
                table: "TInventory",
                column: "Id_item");

            migrationBuilder.CreateIndex(
                name: "IX_TMarket_Id_item",
                table: "TMarket",
                column: "Id_item");

            migrationBuilder.CreateIndex(
                name: "IX_TMarket_Id_seller",
                table: "TMarket",
                column: "Id_seller");
        }
    }
}
