using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Broadcast.Data.Migrations
{
    public partial class _7migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FollowingUserId");

            migrationBuilder.AddColumn<string>(
                name: "MessageTitle",
                table: "Messages",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Flag",
                columns: table => new
                {
                    FlagId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MsgMessageId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flag", x => x.FlagId);
                    table.ForeignKey(
                        name: "FK_Flag_Messages_MsgMessageId",
                        column: x => x.MsgMessageId,
                        principalTable: "Messages",
                        principalColumn: "MessageId");
                });

            migrationBuilder.CreateTable(
                name: "FollowingUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    FollowingUserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowingUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowingUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    VoteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MsgMessageId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    UpVote = table.Column<bool>(type: "INTEGER", nullable: false),
                    DownVote = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => x.VoteId);
                    table.ForeignKey(
                        name: "FK_Vote_Messages_MsgMessageId",
                        column: x => x.MsgMessageId,
                        principalTable: "Messages",
                        principalColumn: "MessageId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flag_MsgMessageId",
                table: "Flag",
                column: "MsgMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowingUser_UserId",
                table: "FollowingUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_MsgMessageId",
                table: "Vote",
                column: "MsgMessageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flag");

            migrationBuilder.DropTable(
                name: "FollowingUser");

            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.DropColumn(
                name: "MessageTitle",
                table: "Messages");

            migrationBuilder.CreateTable(
                name: "FollowingUserId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    FUserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowingUserId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowingUserId_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowingUserId_UserId",
                table: "FollowingUserId",
                column: "UserId");
        }
    }
}
