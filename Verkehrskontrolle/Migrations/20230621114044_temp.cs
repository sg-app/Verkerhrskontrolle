using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Verkehrskontrolle.Migrations
{
    /// <inheritdoc />
    public partial class temp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fahrzeugtyp",
                table: "Fahrzeuge",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fahrzeugtyp",
                table: "Fahrzeuge");
        }
    }
}
