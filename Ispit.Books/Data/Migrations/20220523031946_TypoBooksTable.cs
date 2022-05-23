using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ispit.Books.Data.Migrations
{
    public partial class TypoBooksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthordId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_AuthordId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AuthordId",
                table: "Books");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_AuthorId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "AuthordId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthordId",
                table: "Books",
                column: "AuthordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthordId",
                table: "Books",
                column: "AuthordId",
                principalTable: "Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
