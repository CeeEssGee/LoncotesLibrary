using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoncotesLibrary.Migrations
{
    public partial class UpdateWithCheckoutAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 1,
                column: "OutOfCirculationSince",
                value: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Materials",
                keyColumn: "Id",
                keyValue: 1,
                column: "OutOfCirculationSince",
                value: new DateTime(2023, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
