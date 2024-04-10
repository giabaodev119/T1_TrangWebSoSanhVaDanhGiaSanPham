using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class fixDB07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAddress_Products_ProductId1",
                table: "ProductAddress");

            migrationBuilder.DropIndex(
                name: "IX_ProductAddress_ProductId1",
                table: "ProductAddress");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductAddress");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductAddress",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAddress_ProductId",
                table: "ProductAddress",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAddress_Products_ProductId",
                table: "ProductAddress",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAddress_Products_ProductId",
                table: "ProductAddress");

            migrationBuilder.DropIndex(
                name: "IX_ProductAddress_ProductId",
                table: "ProductAddress");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "ProductAddress",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "ProductAddress",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAddress_ProductId1",
                table: "ProductAddress",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAddress_Products_ProductId1",
                table: "ProductAddress",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
