using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barber.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FIxed_Barber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Barbers_Users_UserId",
                table: "Barbers");

            migrationBuilder.DropIndex(
                name: "IX_Barbers_UserId",
                table: "Barbers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Barbers");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Barbers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Barbers");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Barbers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Barbers_UserId",
                table: "Barbers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Barbers_Users_UserId",
                table: "Barbers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
