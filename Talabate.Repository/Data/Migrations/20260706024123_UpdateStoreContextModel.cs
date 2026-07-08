using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabate.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStoreContextModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_productCategories_ProductCategoryId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_ProductCategoryId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                table: "products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryId",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_ProductCategoryId",
                table: "products",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_productCategories_ProductCategoryId",
                table: "products",
                column: "ProductCategoryId",
                principalTable: "productCategories",
                principalColumn: "Id");
        }
    }
}
