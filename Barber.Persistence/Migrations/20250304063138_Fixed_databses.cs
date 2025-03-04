using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barber.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Fixed_databses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagerUrl",
                table: "Services",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "Imageses",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Barbers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Imageses_BarberId",
                table: "Imageses",
                column: "BarberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Imageses_Barbers_BarberId",
                table: "Imageses",
                column: "BarberId",
                principalTable: "Barbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Imageses_Barbers_BarberId",
                table: "Imageses");

            migrationBuilder.DropIndex(
                name: "IX_Imageses_BarberId",
                table: "Imageses");

            migrationBuilder.DropColumn(
                name: "ImagerUrl",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Barbers");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "Imageses",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}
