using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class _11MIGRATION : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flags_Message_MessageId",
                table: "Flags");

            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                table: "Flags",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Flags_Message_MessageId",
                table: "Flags",
                column: "MessageId",
                principalTable: "Message",
                principalColumn: "MessageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flags_Message_MessageId",
                table: "Flags");

            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                table: "Flags",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Flags_Message_MessageId",
                table: "Flags",
                column: "MessageId",
                principalTable: "Message",
                principalColumn: "MessageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
