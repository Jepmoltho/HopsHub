using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HopsHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class RatingHasUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId",
                table: "Ratings");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 1L,
                column: "UserId",
                value: new Guid("d1ec46ea-b589-4eb3-8b6d-00ed203e7b80"));

            migrationBuilder.UpdateData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 2L,
                column: "UserId",
                value: new Guid("d1ec46ea-b589-4eb3-8b6d-00ed203e7b80"));

            migrationBuilder.UpdateData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 3L,
                column: "UserId",
                value: new Guid("d1ec46ea-b589-4eb3-8b6d-00ed203e7b80"));

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId",
                table: "Ratings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId",
                table: "Ratings");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 1L,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 2L,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 3L,
                column: "UserId",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_UserId",
                table: "Ratings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
