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
            migrationBuilder.InsertData(
                table: "Producto",
                columns: new[] { "Id", "Nombre", "TipoProducto" },
                values: new object[,]
                {
                    { 1, "FPV_BTG_PACTUAL_RECAUDADORA", "FPV" },
                    { 2, "FPV_BTG_PACTUAL_ECOPETROL", "FPV" },
                    { 3, "DEUDAPRIVADA", "FIC" },
                    { 4, "FDO-ACCIONES", "FIC" },
                    { 5, "FPV_BTG_PACTUAL_DINAMICA", "FPV" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Producto",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    1, 2, 3, 4, 5
                });
        }
    }
}
