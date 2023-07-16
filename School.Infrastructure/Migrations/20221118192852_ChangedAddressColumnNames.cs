using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAddressColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentAddress_Street",
                table: "Students",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "StudentAddress_State",
                table: "Students",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "StudentAddress_Country",
                table: "Students",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "StudentAddress_City",
                table: "Students",
                newName: "City");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Students",
                newName: "StudentAddress_Street");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Students",
                newName: "StudentAddress_State");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Students",
                newName: "StudentAddress_Country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Students",
                newName: "StudentAddress_City");
        }
    }
}
