using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Broadcast.Data.Migrations
{
    public partial class _8migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flag_Messages_MsgMessageId",
                table: "Flag");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Messages_MsgMessageId",
                table: "Vote");

            migrationBuilder.RenameColumn(
                name: "MsgMessageId",
                table: "Vote",
                newName: "MessageId");

            migrationBuilder.RenameIndex(
                name: "IX_Vote_MsgMessageId",
                table: "Vote",
                newName: "IX_Vote_MessageId");

            migrationBuilder.RenameColumn(
                name: "MsgMessageId",
                table: "Flag",
                newName: "MessageId");

            migrationBuilder.RenameIndex(
                name: "IX_Flag_MsgMessageId",
                table: "Flag",
                newName: "IX_Flag_MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flag_Messages_MessageId",
                table: "Flag",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Messages_MessageId",
                table: "Vote",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "MessageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flag_Messages_MessageId",
                table: "Flag");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Messages_MessageId",
                table: "Vote");

            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "Vote",
                newName: "MsgMessageId");

            migrationBuilder.RenameIndex(
                name: "IX_Vote_MessageId",
                table: "Vote",
                newName: "IX_Vote_MsgMessageId");

            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "Flag",
                newName: "MsgMessageId");

            migrationBuilder.RenameIndex(
                name: "IX_Flag_MessageId",
                table: "Flag",
                newName: "IX_Flag_MsgMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flag_Messages_MsgMessageId",
                table: "Flag",
                column: "MsgMessageId",
                principalTable: "Messages",
                principalColumn: "MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Messages_MsgMessageId",
                table: "Vote",
                column: "MsgMessageId",
                principalTable: "Messages",
                principalColumn: "MessageId");
        }
    }
}
