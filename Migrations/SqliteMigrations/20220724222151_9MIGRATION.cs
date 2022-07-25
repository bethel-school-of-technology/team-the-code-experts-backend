using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class _9MIGRATION : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlagId",
                table: "Responses");

            migrationBuilder.AddColumn<int>(
                name: "ResponseId",
                table: "Flags",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flags_ResponseId",
                table: "Flags",
                column: "ResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flags_Responses_ResponseId",
                table: "Flags",
                column: "ResponseId",
                principalTable: "Responses",
                principalColumn: "ResponseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flags_Responses_ResponseId",
                table: "Flags");

            migrationBuilder.DropIndex(
                name: "IX_Flags_ResponseId",
                table: "Flags");

            migrationBuilder.DropColumn(
                name: "ResponseId",
                table: "Flags");

            migrationBuilder.AddColumn<int>(
                name: "FlagId",
                table: "Responses",
                type: "INTEGER",
                nullable: true);
        }
    }
}
