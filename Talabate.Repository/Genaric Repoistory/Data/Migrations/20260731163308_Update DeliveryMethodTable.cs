using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabate.Infrastructer.GenaricRepoistory.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeliveryMethodTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DliveryTime",
                table: "deliveryMethods",
                newName: "DeliveryTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveryTime",
                table: "deliveryMethods",
                newName: "DliveryTime");
        }
    }
}
