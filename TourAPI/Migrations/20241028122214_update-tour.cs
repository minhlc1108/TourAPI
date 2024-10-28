using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TourAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatetour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49525abd-e634-4bfe-8c07-0eae6bea6b75");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f33b6939-37bd-4894-8ec7-052e7c62dd18");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Tours",
                newName: "Destination");

            migrationBuilder.AddColumn<string>(
                name: "Departure",
                table: "Tours",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Tours",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3958c5d0-9d2c-4061-94be-ff7f3ee3b5bb", null, "User", "USER" },
                    { "c6dce0b0-1f2d-4165-9472-bf3dbf7971fc", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3958c5d0-9d2c-4061-94be-ff7f3ee3b5bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6dce0b0-1f2d-4165-9472-bf3dbf7971fc");

            migrationBuilder.DropColumn(
                name: "Departure",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Tours");

            migrationBuilder.RenameColumn(
                name: "Destination",
                table: "Tours",
                newName: "City");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "49525abd-e634-4bfe-8c07-0eae6bea6b75", null, "User", "USER" },
                    { "f33b6939-37bd-4894-8ec7-052e7c62dd18", null, "Admin", "ADMIN" }
                });
        }
    }
}
