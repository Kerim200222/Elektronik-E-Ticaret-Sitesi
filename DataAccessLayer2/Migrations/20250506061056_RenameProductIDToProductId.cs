using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer2.Migrations
{
    /// <inheritdoc />
    public partial class RenameProductIDToProductId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductID",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Carts",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_ProductID",
                table: "Carts",
                newName: "IX_Carts_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Carts",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                newName: "IX_Carts_ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_ProductID",
                table: "Carts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
