using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations.Price
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockEntity",
                columns: table => new
                {
                    IDStock = table.Column<Guid>(type: "uuid", nullable: false),
                    IDDivision = table.Column<Guid>(type: "uuid", nullable: false),
                    Stock = table.Column<string>(type: "text", nullable: false),
                    StockName = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Schedule = table.Column<string>(type: "text", nullable: false),
                    CashPayment = table.Column<bool>(type: "boolean", nullable: false),
                    CardPayment = table.Column<bool>(type: "boolean", nullable: false),
                    FIASId = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerInn = table.Column<string>(type: "text", nullable: false),
                    OwnerKpp = table.Column<string>(type: "text", nullable: false),
                    OwnerFullName = table.Column<string>(type: "text", nullable: false),
                    OwnerShortName = table.Column<string>(type: "text", nullable: false),
                    RailwayStation = table.Column<string>(type: "text", nullable: false),
                    ConsigneeCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockEntity", x => x.IDStock);
                });

            migrationBuilder.CreateTable(
                name: "_prices",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    PriceT = table.Column<decimal>(type: "numeric", nullable: false),
                    PriceLimitT1 = table.Column<decimal>(type: "numeric", nullable: true),
                    PriceT1 = table.Column<decimal>(type: "numeric", nullable: true),
                    PriceLimitT2 = table.Column<decimal>(type: "numeric", nullable: true),
                    PriceT2 = table.Column<decimal>(type: "numeric", nullable: true),
                    PriceM = table.Column<decimal>(type: "numeric", nullable: false),
                    PriceLimitM1 = table.Column<decimal>(type: "numeric", nullable: true),
                    PriceM1 = table.Column<decimal>(type: "numeric", nullable: true),
                    PriceLimitM2 = table.Column<decimal>(type: "numeric", nullable: true),
                    PriceM2 = table.Column<decimal>(type: "numeric", nullable: true),
                    NDS = table.Column<int>(type: "integer", nullable: false),
                    IDStock = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__prices", x => x.ID);
                    table.ForeignKey(
                        name: "FK__prices_StockEntity_IDStock",
                        column: x => x.IDStock,
                        principalTable: "StockEntity",
                        principalColumn: "IDStock",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX__prices_IDStock",
                table: "_prices",
                column: "IDStock");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_prices");

            migrationBuilder.DropTable(
                name: "StockEntity");
        }
    }
}
