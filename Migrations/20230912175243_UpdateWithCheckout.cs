using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoncotesLibrary.Migrations
{
    public partial class UpdateWithCheckout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Checkouts",
                columns: new[] { "Id", "CheckoutDate", "MaterialId", "PatronId", "ReturnDate" },
                values: new object[] { 1, new DateTime(2023, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, null });

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 1,
                column: "OutOfCirculationSince",
                value: new DateTime(2023, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Materials_GenreId",
                table: "Materials",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Genres_GenreId",
                table: "Materials",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Genres_GenreId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_GenreId",
                table: "Materials");

            migrationBuilder.DeleteData(
                table: "Checkouts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 1,
                column: "OutOfCirculationSince",
                value: null);
        }
    }
}
