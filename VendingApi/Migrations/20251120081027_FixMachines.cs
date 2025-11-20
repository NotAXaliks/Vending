using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendingApi.Migrations
{
    /// <inheritdoc />
    public partial class FixMachines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachinesSales");

            migrationBuilder.DropTable(
                name: "ProductsSales");

            migrationBuilder.RenameColumn(
                name: "Manufacter",
                table: "Machines",
                newName: "Manufacturer");

            migrationBuilder.AddColumn<string>(
                name: "Modem",
                table: "Machines",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Machines",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_MachineId",
                table: "Sales",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProductId",
                table: "Sales",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Machines_MachineId",
                table: "Sales",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Products_ProductId",
                table: "Sales",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Machines_MachineId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Products_ProductId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_MachineId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ProductId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Modem",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Machines");

            migrationBuilder.RenameColumn(
                name: "Manufacturer",
                table: "Machines",
                newName: "Manufacter");

            migrationBuilder.CreateTable(
                name: "MachinesSales",
                columns: table => new
                {
                    MachinesId = table.Column<int>(type: "integer", nullable: false),
                    SalesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachinesSales", x => new { x.MachinesId, x.SalesId });
                    table.ForeignKey(
                        name: "FK_MachinesSales_Machines_MachinesId",
                        column: x => x.MachinesId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachinesSales_Sales_SalesId",
                        column: x => x.SalesId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsSales",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "integer", nullable: false),
                    SalesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsSales", x => new { x.ProductsId, x.SalesId });
                    table.ForeignKey(
                        name: "FK_ProductsSales_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsSales_Sales_SalesId",
                        column: x => x.SalesId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachinesSales_SalesId",
                table: "MachinesSales",
                column: "SalesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsSales_SalesId",
                table: "ProductsSales",
                column: "SalesId");
        }
    }
}
