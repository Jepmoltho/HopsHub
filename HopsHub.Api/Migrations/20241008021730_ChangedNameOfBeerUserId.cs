using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HopsHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangedNameOfBeerUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brewers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brewers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Brewers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Brewers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Brewers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 7);

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

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Beers",
                newName: "CreatedByUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedByUser",
                table: "Beers",
                newName: "UserId");

            migrationBuilder.InsertData(
                table: "Brewers",
                columns: new[] { "Id", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "Test Brewer", "" },
                    { 2, "Tuborg", "" },
                    { 3, "Carlsberg", "" },
                    { 4, "Mikkeller", "" },
                    { 5, "Guinness", "" }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "Pilsner", "" },
                    { 2, "India Pale Ale", "IPA" },
                    { 3, "Sour", "" },
                    { 4, "Lager", "" },
                    { 5, "Other", "" },
                    { 6, "Stout", "" },
                    { 7, "Porter", "" },
                    { 8, "Wheat Beer", "" },
                    { 9, "Amber Ale", "" },
                    { 10, "Belgian Ale", "" }
                });
        }
    }
}
