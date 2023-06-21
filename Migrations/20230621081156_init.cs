using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Verkehrskontrolle.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fuehrerscheine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gültigkeit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PKWErlaublnis = table.Column<bool>(type: "bit", nullable: false),
                    AnhängerErlaubnis = table.Column<bool>(type: "bit", nullable: false),
                    LKWErlaubnis = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuehrerscheine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Halter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vorname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nachname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Geburtsdatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Straße = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Hausnummer = table.Column<int>(type: "int", nullable: false),
                    Postleitzahl = table.Column<int>(type: "int", nullable: false),
                    Ort = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FührerscheinId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Halter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Halter_Fuehrerscheine_FührerscheinId",
                        column: x => x.FührerscheinId,
                        principalTable: "Fuehrerscheine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fahrzeuge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Antrieb = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sitze = table.Column<int>(type: "int", nullable: false),
                    Leistung = table.Column<int>(type: "int", nullable: false),
                    ZulassungDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TüvDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kennzeichen = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HalterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fahrzeuge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fahrzeuge_Halter_HalterId",
                        column: x => x.HalterId,
                        principalTable: "Halter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fahrzeuge_HalterId",
                table: "Fahrzeuge",
                column: "HalterId");

            migrationBuilder.CreateIndex(
                name: "IX_Halter_FührerscheinId",
                table: "Halter",
                column: "FührerscheinId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fahrzeuge");

            migrationBuilder.DropTable(
                name: "Halter");

            migrationBuilder.DropTable(
                name: "Fuehrerscheine");
        }
    }
}
