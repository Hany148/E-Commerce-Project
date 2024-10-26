using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameOfdeliveryMethods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_deliveryMethodssssss_DeliveryMethodId",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_deliveryMethodssssss",
                table: "deliveryMethodssssss");

            migrationBuilder.RenameTable(
                name: "deliveryMethodssssss",
                newName: "deliveryMethods");

            migrationBuilder.AddPrimaryKey(
                name: "PK_deliveryMethods",
                table: "deliveryMethods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_deliveryMethods_DeliveryMethodId",
                table: "orders",
                column: "DeliveryMethodId",
                principalTable: "deliveryMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_deliveryMethods_DeliveryMethodId",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_deliveryMethods",
                table: "deliveryMethods");

            migrationBuilder.RenameTable(
                name: "deliveryMethods",
                newName: "deliveryMethodssssss");

            migrationBuilder.AddPrimaryKey(
                name: "PK_deliveryMethodssssss",
                table: "deliveryMethodssssss",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_deliveryMethodssssss_DeliveryMethodId",
                table: "orders",
                column: "DeliveryMethodId",
                principalTable: "deliveryMethodssssss",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
