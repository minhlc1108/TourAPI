using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TourAPI.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Bookings_BookingId",
                table: "BookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Customers_CustomerId",
                table: "BookingDetails");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2855ba19-cdd3-487a-b744-34264cce9306");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95e5f734-6355-4970-b11e-e18de4036718");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45b09ebb-a658-4249-b2ce-3966abd08074", null, "User", "USER" },
                    { "ad478c58-1e50-4962-83eb-80e2a9040241", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Bookings_BookingId",
                table: "BookingDetails",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Customers_CustomerId",
                table: "BookingDetails",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Bookings_BookingId",
                table: "BookingDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_Customers_CustomerId",
                table: "BookingDetails");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45b09ebb-a658-4249-b2ce-3966abd08074");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad478c58-1e50-4962-83eb-80e2a9040241");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2855ba19-cdd3-487a-b744-34264cce9306", null, "User", "USER" },
                    { "95e5f734-6355-4970-b11e-e18de4036718", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Bookings_BookingId",
                table: "BookingDetails",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_Customers_CustomerId",
                table: "BookingDetails",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
