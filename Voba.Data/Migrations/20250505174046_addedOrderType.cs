using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voba.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedOrderType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Orders",
                newName: "OrderTypeId");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "OrderCode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "OrderProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Length",
                table: "OrderProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderProducts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Width",
                table: "OrderProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "OrderTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTypes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderCode", "OrderDate", "OrderTypeId", "TotalAmount" },
                values: new object[] { "", new DateTime(2025, 5, 5, 20, 40, 44, 44, DateTimeKind.Local).AddTicks(5804), 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 5, 5, 20, 40, 44, 67, DateTimeKind.Local).AddTicks(6471));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderTypeId",
                table: "Orders",
                column: "OrderTypeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderTypes_OrderTypeId",
                table: "Orders",
                column: "OrderTypeId",
                principalTable: "OrderTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderTypes_OrderTypeId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderTypes");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderTypeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "OrderProducts");

            migrationBuilder.RenameColumn(
                name: "OrderTypeId",
                table: "Orders",
                newName: "Quantity");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Length",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Width",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "Length", "OrderDate", "Quantity", "TotalAmount", "Width" },
                values: new object[] { "Red", 1.5m, new DateTime(2025, 4, 7, 23, 14, 6, 660, DateTimeKind.Local).AddTicks(6767), 10, 500m, 2.5m });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 4, 7, 23, 14, 6, 666, DateTimeKind.Local).AddTicks(5643));
        }
    }
}
