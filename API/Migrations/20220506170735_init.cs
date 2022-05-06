using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Money = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TInventory",
                columns: table => new
                {
                    Id_user = table.Column<int>(type: "int", nullable: false),
                    Id_item = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TInventory", x => new { x.Id_user, x.Id_item });
                    table.ForeignKey(
                        name: "FK_Inventory_Item",
                        column: x => x.Id_item,
                        principalTable: "TItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inventory_User",
                        column: x => x.Id_user,
                        principalTable: "TUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TMarket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_seller = table.Column<int>(type: "int", nullable: false),
                    Id_item = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Is_sold = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMarket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Market_Item",
                        column: x => x.Id_item,
                        principalTable: "TItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Market_User",
                        column: x => x.Id_seller,
                        principalTable: "TUser",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "TItem",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Used for communication and to see events, whether past or future.", "Palantír" },
                    { 2, "Luminescent plasma blade, can cut steel.", "Lightsaber" },
                    { 3, "Use it to transport a small amount of fluid, like coffee.", "Mug" },
                    { 4, "Mhh tasty...", "Stomb's fries" }
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
                table: "TInventory",
                columns: new[] { "Id_item", "Id_user", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 10 },
                    { 3, 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "TMarket",
                columns: new[] { "Id", "Id_item", "Id_seller", "Is_sold", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, false, 500, 1 },
                    { 2, 2, 1, false, 10000, 5 },
                    { 3, 2, 1, true, 100, 1 },
                    { 4, 2, 2, false, 1000, 3 }
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TInventory");

            migrationBuilder.DropTable(
                name: "TMarket");

            migrationBuilder.DropTable(
                name: "TItem");

            migrationBuilder.DropTable(
                name: "TUser");
        }
    }
}
