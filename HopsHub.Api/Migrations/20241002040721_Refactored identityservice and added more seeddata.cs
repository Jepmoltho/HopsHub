using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HopsHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class Refactoredidentityserviceandaddedmoreseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "Alc", "AverageScore", "Description", "Name", "TypeId" },
                values: new object[] { 5, 4.6m, 0m, "", "Tuborg Pilsner", 1 });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[,]
                {
                    { 6, "Stout", "" },
                    { 7, "Porter", "" },
                    { 8, "Wheat Beer", "" },
                    { 9, "Amber Ale", "" },
                    { 10, "Belgian Ale", "" }
                });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "Alc", "AverageScore", "Description", "Name", "TypeId" },
                values: new object[] { 6, 4.6m, 0m, "", "Guiness", 7 });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "BeerId", "Comment", "Score", "UserId" },
                values: new object[,]
                {
                    { 4L, 6, "Black as the night", 0m, new Guid("3157a3d6-47f7-4e1a-bc40-80cec64464e8") },
                    { 5L, 6, "Not very good", 0m, new Guid("3157a3d6-47f7-4e1a-bc40-80cec64464e8") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
