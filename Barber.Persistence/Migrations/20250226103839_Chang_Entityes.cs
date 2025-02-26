using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barber.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Chang_Entityes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Barbers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Barbers",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Barbers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Barbers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Barbers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Barbers");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Barbers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Barbers");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Barbers",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Barbers",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
