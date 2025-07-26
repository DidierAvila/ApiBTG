using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBTG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InsertSucursales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insertar sucursales
            migrationBuilder.InsertData(
                table: "Sucursales",
                columns: new[] { "Id", "Nombre", "Ciudad" },
                values: new object[,]
                {
                    { 1, "Sucursal Centro", "Bogotá" },
                    { 2, "Sucursal Norte", "Medellín" },
                    { 3, "Sucursal Sur", "Cali" },
                    { 4, "Sucursal Este", "Barranquilla" },
                    { 5, "Sucursal Oeste", "Cartagena" },
                    { 6, "Sucursal Central", "Bucaramanga" },
                    { 7, "Sucursal Plaza Mayor", "Pereira" },
                    { 8, "Sucursal Galerías", "Manizales" },
                    { 9, "Sucursal Centro Comercial", "Ibagué" },
                    { 10, "Sucursal Principal", "Villavicencio" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar las sucursales insertadas
            migrationBuilder.DeleteData(
                table: "Sucursales",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9, 10
                });
        }
    }
}
