using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopmanagement.infrastructure.EFCore.Migrations
{
    public partial class initCreateTableProductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Picture = table.Column<string>(maxLength: 1000, nullable: true),
                    PictureAlt = table.Column<string>(maxLength: 255, nullable: true),
                    PictureTitle = table.Column<string>(maxLength: 500, nullable: true),
                    KeyWords = table.Column<string>(maxLength: 255, nullable: false),
                    MetaDescription = table.Column<string>(maxLength: 150, nullable: false),
                    Slug = table.Column<string>(maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCategories");
        }
    }
}
