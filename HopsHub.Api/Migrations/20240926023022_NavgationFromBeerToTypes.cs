using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopsHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class NavgationFromBeerToTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "Alc", "Name", "Rating", "TypeId" },
                values: new object[] { 4, 6.5m, "Other IPA", 0m, 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
