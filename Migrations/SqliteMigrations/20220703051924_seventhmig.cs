using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class seventhmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowingUser_Users_AppUserId",
                table: "FollowingUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_AppUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Users_AppUserId",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_AppUserId",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AppUserId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FollowingUser",
                table: "FollowingUser");

            migrationBuilder.DropIndex(
                name: "IX_FollowingUser_AppUserId",
                table: "FollowingUser");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "FollowingUser");

            migrationBuilder.RenameTable(
                name: "FollowingUser",
                newName: "FollowingUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Vote",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Messages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "FollowingUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FollowingUsers",
                table: "FollowingUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowingUsers_UserId",
                table: "FollowingUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowingUsers_Users_UserId",
                table: "FollowingUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowingUsers_Users_UserId",
                table: "FollowingUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FollowingUsers",
                table: "FollowingUsers");

            migrationBuilder.DropIndex(
                name: "IX_FollowingUsers_UserId",
                table: "FollowingUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FollowingUsers");

            migrationBuilder.RenameTable(
                name: "FollowingUsers",
                newName: "FollowingUser");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Vote",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Messages",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "FollowingUser",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FollowingUser",
                table: "FollowingUser",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_AppUserId",
                table: "Vote",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AppUserId",
                table: "Messages",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowingUser_AppUserId",
                table: "FollowingUser",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowingUser_Users_AppUserId",
                table: "FollowingUser",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_AppUserId",
                table: "Messages",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Users_AppUserId",
                table: "Vote",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
