using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voba.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedTotalValuesToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalAmount",
                table: "Orders",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalSquareMeter",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "TotalAmount", "TotalSquareMeter" },
                values: new object[] { new DateTime(2025, 9, 5, 20, 14, 55, 669, DateTimeKind.Local).AddTicks(2714), null, 0.0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 9, 5, 20, 14, 55, 680, DateTimeKind.Local).AddTicks(7535));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalSquareMeter",
                table: "Orders");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "TotalAmount" },
                values: new object[] { new DateTime(2025, 9, 4, 18, 54, 29, 470, DateTimeKind.Local).AddTicks(7340), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 9, 4, 18, 54, 29, 482, DateTimeKind.Local).AddTicks(3667));
        }
    }
}
