using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopmanagement.infrastructure.EFCore.Migrations
{
    public partial class eiteProdutCatgoryMxLenghDescriptionAndMetaDescrriptin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MetaDescription",
                table: "ProductCategories",
                maxLength: 1500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductCategories",
                maxLength: 1500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MetaDescription",
                table: "ProductCategories",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1500);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductCategories",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1500,
                oldNullable: true);
        }
    }
}
