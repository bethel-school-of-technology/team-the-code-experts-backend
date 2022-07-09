using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations.SqliteMigrations
{
    public partial class mig12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flag_Messages_MessageId",
                table: "Flag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flag",
                table: "Flag");

            migrationBuilder.RenameTable(
                name: "Flag",
                newName: "Flags");

            migrationBuilder.RenameIndex(
                name: "IX_Flag_MessageId",
                table: "Flags",
                newName: "IX_Flags_MessageId");

            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                table: "Flags",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flags",
                table: "Flags",
                column: "FlagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flags_Messages_MessageId",
                table: "Flags",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "MessageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flags_Messages_MessageId",
                table: "Flags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flags",
                table: "Flags");

            migrationBuilder.RenameTable(
                name: "Flags",
                newName: "Flag");

            migrationBuilder.RenameIndex(
                name: "IX_Flags_MessageId",
                table: "Flag",
                newName: "IX_Flag_MessageId");

            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                table: "Flag",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flag",
                table: "Flag",
                column: "FlagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flag_Messages_MessageId",
                table: "Flag",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "MessageId");
        }
    }
}
