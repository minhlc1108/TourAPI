using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TourAPI.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourImages_Tours_TourId",
                table: "TourImages");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3be0c6a7-fb10-4171-9658-fe0c40ec815c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef20c89e-0cf8-406f-ba78-6422a5f1f5f6");

            migrationBuilder.DropColumn(
                name: "IsAvatar",
                table: "TourImages");

            migrationBuilder.AlterColumn<int>(
                name: "TourId",
                table: "TourImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "03433060-cc9e-4194-8c0c-1b5f15002376", null, "User", "USER" },
                    { "5abf4941-d44a-4df2-9ae1-dee5beecf9d8", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TourImages_Tours_TourId",
                table: "TourImages",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourImages_Tours_TourId",
                table: "TourImages");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03433060-cc9e-4194-8c0c-1b5f15002376");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5abf4941-d44a-4df2-9ae1-dee5beecf9d8");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "TourId",
                table: "TourImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvatar",
                table: "TourImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3be0c6a7-fb10-4171-9658-fe0c40ec815c", null, "User", "USER" },
                    { "ef20c89e-0cf8-406f-ba78-6422a5f1f5f6", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TourImages_Tours_TourId",
                table: "TourImages",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id");
        }
    }
}
