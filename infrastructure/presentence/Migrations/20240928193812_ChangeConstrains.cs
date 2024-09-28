using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeConstrains : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_productBrands_BrandID",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_productTypes_ProductTypeID",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "BrandID",
                table: "products",
                newName: "BrandId");

            migrationBuilder.RenameColumn(
                name: "ProductTypeID",
                table: "products",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_products_BrandID",
                table: "products",
                newName: "IX_products_BrandId");

            migrationBuilder.RenameIndex(
                name: "IX_products_ProductTypeID",
                table: "products",
                newName: "IX_products_TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_productBrands_BrandId",
                table: "products",
                column: "BrandId",
                principalTable: "productBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_productTypes_TypeId",
                table: "products",
                column: "TypeId",
                principalTable: "productTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_productBrands_BrandId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_productTypes_TypeId",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "products",
                newName: "BrandID");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "products",
                newName: "ProductTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_products_BrandId",
                table: "products",
                newName: "IX_products_BrandID");

            migrationBuilder.RenameIndex(
                name: "IX_products_TypeId",
                table: "products",
                newName: "IX_products_ProductTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_products_productBrands_BrandID",
                table: "products",
                column: "BrandID",
                principalTable: "productBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_products_productTypes_ProductTypeID",
                table: "products",
                column: "ProductTypeID",
                principalTable: "productTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
