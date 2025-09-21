using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voba.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateOrderDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Renk = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Boy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    En = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Adet = table.Column<int>(type: "int", nullable: true),
                    Metrekare = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    MetrekareFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ToplamFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "Status" },
                values: new object[] { new DateTime(2025, 8, 25, 20, 7, 21, 230, DateTimeKind.Local).AddTicks(2106), "Beklemede" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 8, 25, 20, 7, 21, 233, DateTimeKind.Local).AddTicks(8314));

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "Status" },
                values: new object[] { new DateTime(2025, 8, 13, 20, 55, 46, 940, DateTimeKind.Local).AddTicks(7548), 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2025, 8, 13, 20, 55, 46, 955, DateTimeKind.Local).AddTicks(846));
        }
    }
}
