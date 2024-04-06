using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class fixDb03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IActive",
                table: "Products",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IActive",
                table: "Posts",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IActive",
                table: "News",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IActive",
                table: "Categories",
                newName: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Products",
                newName: "IActive");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Posts",
                newName: "IActive");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "News",
                newName: "IActive");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Categories",
                newName: "IActive");
        }
    }
}
