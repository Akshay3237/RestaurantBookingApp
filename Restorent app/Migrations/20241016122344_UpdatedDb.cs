using Microsoft.EntityFrameworkCore.Migrations;

namespace Restorent_app.Migrations
{
    public partial class UpdatedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Tables_TableId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Books",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_UserId",
                table: "Books",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Tables_TableId",
                table: "Books",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Tableid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_UserId",
                table: "Books",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Tables_TableId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_UserId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_UserId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Books");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Tables_TableId",
                table: "Books",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Tableid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
