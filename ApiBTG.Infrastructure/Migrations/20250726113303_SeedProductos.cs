using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBTG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insertar datos de ejemplo en la tabla Productos
            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Nombre", "TipoProducto" },
                values: new object[,]
                {
                    { "FPV_BTG_PACTUAL_RECAUDADORA", "FPV" },
                    { "FPV_BTG_PACTUAL_ECOPETROL", "FPV" },
                    { "DEUDAPRIVADA", "FIC" },
                    { "FDO-ACCIONES", "FIC" },
                    { "FPV_BTG_PACTUAL_DINAMICA", "FPV" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar los datos insertados
            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Nombre",
                keyValues: new object[]
                {
                    "FPV_BTG_PACTUAL_RECAUDADORA",
                    "FPV_BTG_PACTUAL_ECOPETROL",
                    "DEUDAPRIVADA",
                    "FDO-ACCIONES",
                    "FPV_BTG_PACTUAL_DINAMICA"
                });
        }
    }
}
