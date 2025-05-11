using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer2.Migrations
{
    /// <inheritdoc />
    public partial class AddPricePrecisionAndOtherPendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UrunId",
                table: "Carts",
                newName: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductID",
                table: "Carts",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_ProductID",
                table: "Carts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductID",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ProductID",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Carts",
                newName: "UrunId");
        }
    }
}
