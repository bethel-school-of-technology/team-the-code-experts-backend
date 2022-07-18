using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class _4migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowingUsers_Users_UserId",
                table: "FollowingUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Message_MessageId",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Responses_ResponseId",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_FollowingUsers_UserId",
                table: "FollowingUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FollowingUsers");

            migrationBuilder.AlterColumn<int>(
                name: "ResponseId",
                table: "Vote",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                table: "Vote",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "MessageResponseId",
                table: "Vote",
                type: "INTEGER",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Message_MessageId",
                table: "Vote",
                column: "MessageId",
                principalTable: "Message",
                principalColumn: "MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Responses_ResponseId",
                table: "Vote",
                column: "ResponseId",
                principalTable: "Responses",
                principalColumn: "ResponseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowingUsers_Users_AppUserId",
                table: "FollowingUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Message_MessageId",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Responses_ResponseId",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_FollowingUsers_AppUserId",
                table: "FollowingUsers");

            migrationBuilder.DropColumn(
                name: "MessageResponseId",
                table: "Vote");

            migrationBuilder.AlterColumn<int>(
                name: "ResponseId",
                table: "Vote",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                table: "Vote",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Message_MessageId",
                table: "Vote",
                column: "MessageId",
                principalTable: "Message",
                principalColumn: "MessageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Responses_ResponseId",
                table: "Vote",
                column: "ResponseId",
                principalTable: "Responses",
                principalColumn: "ResponseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
