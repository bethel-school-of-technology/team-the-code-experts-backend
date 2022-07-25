using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class _10MIGRATION : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Flags");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Flags",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FlagTypes",
                columns: table => new
                {
                    FlagTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlagTypes", x => x.FlagTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flags_AppUserId",
                table: "Flags",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flags_Users_AppUserId",
                table: "Flags",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flags_Users_AppUserId",
                table: "Flags");

            migrationBuilder.DropTable(
                name: "FlagTypes");

            migrationBuilder.DropIndex(
                name: "IX_Flags_AppUserId",
                table: "Flags");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Flags");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Flags",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
