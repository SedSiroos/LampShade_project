using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CommentManagement.Infrastructure.EFCore.Migrations
{
    public partial class NewComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Email = table.Column<string>(maxLength: 500, nullable: false),
                    Phone = table.Column<string>(maxLength: 14, nullable: true),
                    Message = table.Column<string>(nullable: false),
                    IsConfirmed = table.Column<bool>(nullable: false),
                    IsCanceled = table.Column<bool>(nullable: false),
                    OwnerRecordId = table.Column<long>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    ParentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentId",
                table: "Comments",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");
        }
    }
}
