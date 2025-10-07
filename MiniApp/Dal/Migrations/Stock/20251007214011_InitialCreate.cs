using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations.Stock
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_stocks",
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
                    table.PrimaryKey("PK__stocks", x => x.IDStock);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_stocks");
        }
    }
}
