using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DACS.Migrations
{
    /// <inheritdoc />
    public partial class FixDB27 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Categories_CategoryId",
                table: "News");

            migrationBuilder.DropTable(
                name: "SystemSettings");

            migrationBuilder.DropIndex(
                name: "IX_News_CategoryId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "SeoDescription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SeoKeywords",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SeoTitle",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SeoDescription",
                table: "ProductCategory");

            migrationBuilder.DropColumn(
                name: "SeoKeywords",
                table: "ProductCategory");

            migrationBuilder.DropColumn(
                name: "SeoTitle",
                table: "ProductCategory");

            migrationBuilder.DropColumn(
                name: "SeoDescription",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SeoKeywords",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SeoTitle",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SeoDescription",
                table: "News");

            migrationBuilder.DropColumn(
                name: "SeoKeywords",
                table: "News");

            migrationBuilder.DropColumn(
                name: "SeoTitle",
                table: "News");

            migrationBuilder.DropColumn(
                name: "SeoDescription",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "SeoKeywords",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "SeoTitle",
                table: "Categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SeoDescription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoKeywords",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitle",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoDescription",
                table: "ProductCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeoKeywords",
                table: "ProductCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeoTitle",
                table: "ProductCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeoDescription",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoKeywords",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitle",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoDescription",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoKeywords",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitle",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoDescription",
                table: "Categories",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoKeywords",
                table: "Categories",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitle",
                table: "Categories",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SystemSettings",
                columns: table => new
                {
                    SettingKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SettingDescripstion = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    SettingValue = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSettings", x => x.SettingKey);
                });

            migrationBuilder.CreateIndex(
                name: "IX_News_CategoryId",
                table: "News",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Categories_CategoryId",
                table: "News",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
