using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBTG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MoveMontoMinimoToDisponibilidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MontoMinimo",
                table: "Inscripciones");

            migrationBuilder.AddColumn<decimal>(
                name: "MontoMinimo",
                table: "Disponibilidades",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MontoMinimo",
                table: "Disponibilidades");

            migrationBuilder.AddColumn<decimal>(
                name: "MontoMinimo",
                table: "Inscripciones",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
