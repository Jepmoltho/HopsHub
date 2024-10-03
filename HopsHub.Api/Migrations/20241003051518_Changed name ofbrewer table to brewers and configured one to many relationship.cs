using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopsHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class Changednameofbrewertabletobrewersandconfiguredonetomanyrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_Brewer_BrewerId",
                table: "Beers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brewer",
                table: "Brewer");

            migrationBuilder.RenameTable(
                name: "Brewer",
                newName: "Brewers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brewers",
                table: "Brewers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_Brewers_BrewerId",
                table: "Beers",
                column: "BrewerId",
                principalTable: "Brewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_Brewers_BrewerId",
                table: "Beers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brewers",
                table: "Brewers");

            migrationBuilder.RenameTable(
                name: "Brewers",
                newName: "Brewer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brewer",
                table: "Brewer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_Brewer_BrewerId",
                table: "Beers",
                column: "BrewerId",
                principalTable: "Brewer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
