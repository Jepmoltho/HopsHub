﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HopsHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class Brewertabelandrelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrewerId",
                table: "Beers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Brewer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brewer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beers_BrewerId",
                table: "Beers",
                column: "BrewerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_Brewer_BrewerId",
                table: "Beers",
                column: "BrewerId",
                principalTable: "Brewer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_Brewer_BrewerId",
                table: "Beers");

            migrationBuilder.DropTable(
                name: "Brewer");

            migrationBuilder.DropIndex(
                name: "IX_Beers_BrewerId",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "BrewerId",
                table: "Beers");
        }
    }
}
