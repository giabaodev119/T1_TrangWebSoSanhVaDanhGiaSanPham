using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class fixDB19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Author",
                table: "productComments",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "productComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_productComments_ProductId",
                table: "productComments",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_productComments_Products_ProductId",
                table: "productComments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productComments_Products_ProductId",
                table: "productComments");

            migrationBuilder.DropIndex(
                name: "IX_productComments_ProductId",
                table: "productComments");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "productComments");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "productComments",
                newName: "Author");
        }
    }
}
