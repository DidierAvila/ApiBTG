using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBTG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDisponibilidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insertar disponibilidades de ejemplo
            migrationBuilder.InsertData(
                table: "Disponibilidades",
                columns: new[] { "IdSucursal", "IdProducto", "MontoMinimo" },
                values: new object[,]
                {
                    { 1, 1, 75000m },    // Sucursal Centro (Bogotá) - FPV_BTG_PACTUAL_RECAUDADORA
                    { 2, 2, 125000m },  // Sucursal Norte (Bogotá) - FPV_BTG_PACTUAL_ECOPETROL
                    { 5, 3, 50000m },   // Sucursal Poblado (Medellín) - DEUDAPRIVADA
                    { 9, 4, 250000m },  // Sucursal Granada (Cali) - FDO-ACCIONES
                    { 15, 5, 100000m }, // Sucursal Centro (Pereira) - FPV_BTG_PACTUAL_DINAMICA
                    { 6, 1, 80000m }    // Sucursal Laureles (Medellín) - FPV_BTG_PACTUAL_RECAUDADORA
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar las disponibilidades insertadas
            migrationBuilder.DeleteData(
                table: "Disponibilidades",
                keyColumns: new[] { "IdSucursal", "IdProducto" },
                keyValues: new object[] { 1, 1 });
            migrationBuilder.DeleteData(
                table: "Disponibilidades",
                keyColumns: new[] { "IdSucursal", "IdProducto" },
                keyValues: new object[] { 2, 2 });
            migrationBuilder.DeleteData(
                table: "Disponibilidades",
                keyColumns: new[] { "IdSucursal", "IdProducto" },
                keyValues: new object[] { 5, 3 });
            migrationBuilder.DeleteData(
                table: "Disponibilidades",
                keyColumns: new[] { "IdSucursal", "IdProducto" },
                keyValues: new object[] { 9, 4 });
            migrationBuilder.DeleteData(
                table: "Disponibilidades",
                keyColumns: new[] { "IdSucursal", "IdProducto" },
                keyValues: new object[] { 15, 5 });
            migrationBuilder.DeleteData(
                table: "Disponibilidades",
                keyColumns: new[] { "IdSucursal", "IdProducto" },
                keyValues: new object[] { 6, 1 });
        }
    }
}
