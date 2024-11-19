using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TourAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatepromotion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3491e7ce-df06-4819-ab82-42597fe6b759");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7355fa0a-194b-4aa2-aaa9-18a1c685e21d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "202dff15-6bc7-4a8f-8e21-4f179d7be4cd", null, "User", "USER" },
                    { "282b52cb-8a81-4a1c-926c-5d34ca6b39b1", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "202dff15-6bc7-4a8f-8e21-4f179d7be4cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "282b52cb-8a81-4a1c-926c-5d34ca6b39b1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3491e7ce-df06-4819-ab82-42597fe6b759", null, "User", "USER" },
                    { "7355fa0a-194b-4aa2-aaa9-18a1c685e21d", null, "Admin", "ADMIN" }
                });
        }
    }
}
