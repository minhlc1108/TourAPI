using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TourAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e564d3d-101e-44f9-9fbc-15cb8ad8289c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82368b98-b8e6-4f58-b1f2-f15db2059eee");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "TourSchedules",
                newName: "Remain");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Tours",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3be0c6a7-fb10-4171-9658-fe0c40ec815c", null, "User", "USER" },
                    { "ef20c89e-0cf8-406f-ba78-6422a5f1f5f6", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3be0c6a7-fb10-4171-9658-fe0c40ec815c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef20c89e-0cf8-406f-ba78-6422a5f1f5f6");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Tours");

            migrationBuilder.RenameColumn(
                name: "Remain",
                table: "TourSchedules",
                newName: "Quantity");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e564d3d-101e-44f9-9fbc-15cb8ad8289c", null, "Admin", "ADMIN" },
                    { "82368b98-b8e6-4f58-b1f2-f15db2059eee", null, "User", "USER" }
                });
        }
    }
}
