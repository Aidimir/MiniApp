using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations.Nomenclature
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypeEntity",
                columns: table => new
                {
                    IDType = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IDParentType = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeEntity", x => x.IDType);
                });

            migrationBuilder.CreateTable(
                name: "_nomenclatures",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    IDCat = table.Column<string>(type: "text", nullable: false),
                    IDTypeNew = table.Column<string>(type: "text", nullable: false),
                    ProductionType = table.Column<string>(type: "text", nullable: false),
                    IDFunctionType = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Gost = table.Column<string>(type: "text", nullable: false),
                    FormOfLength = table.Column<string>(type: "text", nullable: false),
                    Manufacturer = table.Column<string>(type: "text", nullable: false),
                    SteelGrade = table.Column<string>(type: "text", nullable: false),
                    Diameter = table.Column<decimal>(type: "numeric", nullable: false),
                    ProfileSize2 = table.Column<decimal>(type: "numeric", nullable: true),
                    PipeWallThickness = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Koef = table.Column<decimal>(type: "numeric", nullable: false),
                    IDType = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__nomenclatures", x => x.ID);
                    table.ForeignKey(
                        name: "FK__nomenclatures_TypeEntity_IDType",
                        column: x => x.IDType,
                        principalTable: "TypeEntity",
                        principalColumn: "IDType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX__nomenclatures_IDType",
                table: "_nomenclatures",
                column: "IDType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_nomenclatures");

            migrationBuilder.DropTable(
                name: "TypeEntity");
        }
    }
}
