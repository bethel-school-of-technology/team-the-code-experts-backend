using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class _2migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowingUsers_Users_AppUserId",
                table: "FollowingUsers");

            migrationBuilder.DropIndex(
                name: "IX_FollowingUsers_AppUserId",
                table: "FollowingUsers");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "FollowingUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "FollowingUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FollowingUsers_UserId",
                table: "FollowingUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowingUsers_Users_UserId",
                table: "FollowingUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowingUsers_Users_UserId",
                table: "FollowingUsers");

            migrationBuilder.DropIndex(
                name: "IX_FollowingUsers_UserId",
                table: "FollowingUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FollowingUsers");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "FollowingUsers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_FollowingUsers_AppUserId",
                table: "FollowingUsers",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowingUsers_Users_AppUserId",
                table: "FollowingUsers",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
