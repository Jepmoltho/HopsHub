using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HopsHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class BeerTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "Beers",
                type: "decimal(3,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Beers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Rating", "TypeId" },
                values: new object[] { 0m, 2 });

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Rating", "TypeId" },
                values: new object[] { 0m, 4 });

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Rating", "TypeId" },
                values: new object[] { 0m, 3 });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "Name", "ShortName" },
                values: new object[,]
                {
                    { 1, "Pilsner", "" },
                    { 2, "India Pale Ale", "IPA" },
                    { 3, "Sour", "" },
                    { 4, "Lager", "" },
                    { 5, "Other", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beers_TypeId",
                table: "Beers",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_Types_TypeId",
                table: "Beers",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_Types_TypeId",
                table: "Beers");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropIndex(
                name: "IX_Beers_TypeId",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Beers");
        }
    }
}
