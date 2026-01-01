using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MilitaryApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeKingOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Militaries_Kings_KingId",
                table: "Militaries");

            migrationBuilder.AlterColumn<int>(
                name: "KingId",
                table: "Militaries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Militaries_Kings_KingId",
                table: "Militaries",
                column: "KingId",
                principalTable: "Kings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Militaries_Kings_KingId",
                table: "Militaries");

            migrationBuilder.AlterColumn<int>(
                name: "KingId",
                table: "Militaries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Militaries_Kings_KingId",
                table: "Militaries",
                column: "KingId",
                principalTable: "Kings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
