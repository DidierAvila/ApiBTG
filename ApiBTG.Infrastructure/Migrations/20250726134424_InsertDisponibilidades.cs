using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBTG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InsertDisponibilidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // FPV_BTG_PACTUAL_RECAUDADORA - COP $75.000
            migrationBuilder.InsertData(
                table: "Disponibilidad",
                columns: new[] { "IdSucursal", "IdProducto", "MontoMinimo" },
                values: new object[,]
                {
                    { 1, 1, 75000.00m }, // Bogotá
                    { 2, 1, 75000.00m }, // Medellín
                    { 3, 1, 75000.00m }, // Cali
                    { 4, 1, 75000.00m }, // Barranquilla
                    { 5, 1, 75000.00m }  // Cartagena
                });

            // FPV_BTG_PACTUAL_ECOPETROL - COP $125.000
            migrationBuilder.InsertData(
                table: "Disponibilidad",
                columns: new[] { "IdSucursal", "IdProducto", "MontoMinimo" },
                values: new object[,]
                {
                    { 1, 2, 125000.00m }, // Bogotá
                    { 2, 2, 125000.00m }, // Medellín
                    { 3, 2, 125000.00m }, // Cali
                    { 6, 2, 125000.00m }, // Bucaramanga
                    { 7, 2, 125000.00m }  // Pereira
                });

            // DEUDAPRIVADA - COP $50.000
            migrationBuilder.InsertData(
                table: "Disponibilidad",
                columns: new[] { "IdSucursal", "IdProducto", "MontoMinimo" },
                values: new object[,]
                {
                    { 1, 3, 50000.00m }, // Bogotá
                    { 2, 3, 50000.00m }, // Medellín
                    { 8, 3, 50000.00m }, // Manizales
                    { 9, 3, 50000.00m }, // Villavicencio
                    { 10, 3, 50000.00m } // Pasto
                });

            // FDO-ACCIONES - COP $250.000
            migrationBuilder.InsertData(
                table: "Disponibilidad",
                columns: new[] { "IdSucursal", "IdProducto", "MontoMinimo" },
                values: new object[,]
                {
                    { 1, 4, 250000.00m }, // Bogotá
                    { 2, 4, 250000.00m }, // Medellín
                    { 3, 4, 250000.00m }, // Cali
                    { 4, 4, 250000.00m }, // Barranquilla
                    { 5, 4, 250000.00m }  // Cartagena
                });

            // FPV_BTG_PACTUAL_DINAMICA - COP $100.000
            migrationBuilder.InsertData(
                table: "Disponibilidad",
                columns: new[] { "IdSucursal", "IdProducto", "MontoMinimo" },
                values: new object[,]
                {
                    { 1, 5, 100000.00m }, // Bogotá
                    { 2, 5, 100000.00m }, // Medellín
                    { 6, 5, 100000.00m }, // Bucaramanga
                    { 7, 5, 100000.00m }, // Pereira
                    { 8, 5, 100000.00m }  // Manizales
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar todas las disponibilidades insertadas
            migrationBuilder.DeleteData(
                table: "Disponibilidad",
                keyColumns: new[] { "IdSucursal", "IdProducto" },
                keyValues: new object[,]
                {
                    // FPV_BTG_PACTUAL_RECAUDADORA
                    { 1, 1 }, { 2, 1 }, { 3, 1 }, { 4, 1 }, { 5, 1 },
                    // FPV_BTG_PACTUAL_ECOPETROL
                    { 1, 2 }, { 2, 2 }, { 3, 2 }, { 6, 2 }, { 7, 2 },
                    // DEUDAPRIVADA
                    { 1, 3 }, { 2, 3 }, { 8, 3 }, { 9, 3 }, { 10, 3 },
                    // FDO-ACCIONES
                    { 1, 4 }, { 2, 4 }, { 3, 4 }, { 4, 4 }, { 5, 4 },
                    // FPV_BTG_PACTUAL_DINAMICA
                    { 1, 5 }, { 2, 5 }, { 6, 5 }, { 7, 5 }, { 8, 5 }
                });
        }
    }
}
