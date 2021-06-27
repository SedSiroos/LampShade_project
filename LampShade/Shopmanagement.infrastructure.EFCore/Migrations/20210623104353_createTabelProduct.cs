using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopmanagement.infrastructure.EFCore.Migrations
{
    public partial class createTabelProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 500, nullable: false),
                    Code = table.Column<string>(maxLength: 15, nullable: false),
                    UnitPrice = table.Column<string>(nullable: true),
                    IsInStock = table.Column<bool>(nullable: false),
                    ShortDescription = table.Column<string>(maxLength: 500, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Picture = table.Column<string>(maxLength: 1000, nullable: false),
                    PictureAlt = table.Column<string>(maxLength: 250, nullable: true),
                    PictureTitle = table.Column<string>(maxLength: 500, nullable: true),
                    Slug = table.Column<string>(maxLength: 500, nullable: false),
                    Keywords = table.Column<string>(maxLength: 100, nullable: false),
                    MetaDescription = table.Column<string>(maxLength: 1000, nullable: false),
                    CategoryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
