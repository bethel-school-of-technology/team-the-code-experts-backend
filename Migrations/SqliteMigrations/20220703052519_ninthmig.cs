using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class ninthmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flag_Users_AppUserId",
                table: "Flag");

            migrationBuilder.DropIndex(
                name: "IX_Flag_AppUserId",
                table: "Flag");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Flag");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Flag",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Flag");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Flag",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flag_AppUserId",
                table: "Flag",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flag_Users_AppUserId",
                table: "Flag",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
