using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBTG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InsertProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insertar productos
            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Nombre", "MontoMinimo", "TipoProducto" },
                values: new object[,]
                {
                    { 1, "FPV_BTG_PACTUAL_RECAUDADORA", 75000.00m, "FPV" },
                    { 2, "FPV_BTG_PACTUAL_ECOPETROL", 125000.00m, "FPV" },
                    { 3, "DEUDAPRIVADA", 50000.00m, "FIC" },
                    { 4, "FDO-ACCIONES", 250000.00m, "FIC" },
                    { 5, "FPV_BTG_PACTUAL_DINAMICA", 100000.00m, "FPV" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar los productos insertados
            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    1, 2, 3, 4, 5
                });
        }
    }
}
