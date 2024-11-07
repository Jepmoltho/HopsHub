using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopsHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class DeletedProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Types",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Ratings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Brewers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Beers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Brewers");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Beers");
        }
    }
}
