using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HopsHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class BeerUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Beers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Beers");

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "Alc", "AverageScore", "BrewerId", "Description", "Name", "TypeId" },
                values: new object[,]
                {
                    { 1, 6.5m, 0m, 1, "", "Sample IPA", 2 },
                    { 2, 5.0m, 0m, 1, "", "Crispy Lager", 4 },
                    { 3, 4.2m, 0m, 1, "", "Tart Sour", 3 },
                    { 4, 6.5m, 0m, 1, "", "Other IPA", 2 },
                    { 5, 4.6m, 0m, 2, "", "Tuborg Pilsner", 1 },
                    { 6, 4.6m, 0m, 5, "", "Guinness Draught", 7 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "BeerId", "Comment", "Score", "UserId" },
                values: new object[,]
                {
                    { 1L, 1, "Nice and bitter IPA", 0m, new Guid("d1ec46ea-b589-4eb3-8b6d-00ed203e7b80") },
                    { 2L, 2, "Heavy and dark Lager", 0m, new Guid("d1ec46ea-b589-4eb3-8b6d-00ed203e7b80") },
                    { 3L, 3, "So sour it made my eyes squint", 0m, new Guid("d1ec46ea-b589-4eb3-8b6d-00ed203e7b80") },
                    { 4L, 6, "Black as the night", 0m, new Guid("3157a3d6-47f7-4e1a-bc40-80cec64464e8") },
                    { 5L, 6, "Not very good", 0m, new Guid("3157a3d6-47f7-4e1a-bc40-80cec64464e8") }
                });
        }
    }
}
