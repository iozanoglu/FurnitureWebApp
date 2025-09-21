using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voba.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixedOrderType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 9, 4, 18, 47, 34, 6, DateTimeKind.Local).AddTicks(6172));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 9, 4, 18, 47, 34, 17, DateTimeKind.Local).AddTicks(7556));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 8, 25, 20, 7, 21, 230, DateTimeKind.Local).AddTicks(2106));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 8, 25, 20, 7, 21, 233, DateTimeKind.Local).AddTicks(8314));
        }
    }
}
