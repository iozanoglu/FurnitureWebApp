using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voba.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedModelNameAndIsActiveColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "OrderTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "OrderProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModelName",
                table: "OrderProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsActive", "OrderDate" },
                values: new object[] { true, new DateTime(2025, 6, 29, 14, 21, 30, 711, DateTimeKind.Local).AddTicks(9403) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "IsActive" },
                values: new object[] { new DateTime(2025, 6, 29, 14, 21, 30, 726, DateTimeKind.Local).AddTicks(2400), true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "OrderTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "ModelName",
                table: "OrderProducts");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 6, 29, 13, 58, 13, 961, DateTimeKind.Local).AddTicks(427));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "IsActive" },
                values: new object[] { new DateTime(2025, 6, 29, 13, 58, 13, 982, DateTimeKind.Local).AddTicks(8188), false });
        }
    }
}
