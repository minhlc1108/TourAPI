using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TourAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "aa6cbfbd-af79-444f-943c-1c92ebb56fb3", null, "Admin", "ADMIN" },
                    { "d57f31b4-c6f8-4fa5-9c76-24203ee31dd2", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa6cbfbd-af79-444f-943c-1c92ebb56fb3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d57f31b4-c6f8-4fa5-9c76-24203ee31dd2");

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
