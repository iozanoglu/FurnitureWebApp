using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voba.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStatusToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "Status" },
                values: new object[] { new DateTime(2025, 6, 15, 14, 10, 18, 157, DateTimeKind.Local).AddTicks(1615), "Beklemede" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 6, 15, 14, 10, 18, 163, DateTimeKind.Local).AddTicks(1670));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "Status" },
                values: new object[] { new DateTime(2025, 6, 13, 14, 21, 7, 233, DateTimeKind.Local).AddTicks(8016), "Pending" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 6, 13, 14, 21, 7, 256, DateTimeKind.Local).AddTicks(1959));
        }
    }
}
