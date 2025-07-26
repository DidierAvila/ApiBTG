using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBTG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedSucursales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insertar datos de ejemplo en la tabla Sucursales
            migrationBuilder.InsertData(
                table: "Sucursales",
                columns: new[] { "Nombre", "Ciudad" },
                values: new object[,]
                {
                    { "Sucursal Centro", "Bogotá" },
                    { "Sucursal Norte", "Bogotá" },
                    { "Sucursal Sur", "Bogotá" },
                    { "Sucursal Occidente", "Bogotá" },
                    { "Sucursal Poblado", "Medellín" },
                    { "Sucursal Laureles", "Medellín" },
                    { "Sucursal Centro Histórico", "Cartagena" },
                    { "Sucursal Bocagrande", "Cartagena" },
                    { "Sucursal Granada", "Cali" },
                    { "Sucursal San Fernando", "Cali" },
                    { "Sucursal Centro Comercial", "Barranquilla" },
                    { "Sucursal Norte", "Barranquilla" },
                    { "Sucursal Principal", "Bucaramanga" },
                    { "Sucursal Cabecera", "Bucaramanga" },
                    { "Sucursal Centro", "Pereira" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar los datos insertados
            migrationBuilder.DeleteData(
                table: "Sucursales",
                keyColumn: "Nombre",
                keyValues: new object[]
                {
                    "Sucursal Centro",
                    "Sucursal Norte", 
                    "Sucursal Sur",
                    "Sucursal Occidente",
                    "Sucursal Poblado",
                    "Sucursal Laureles",
                    "Sucursal Centro Histórico",
                    "Sucursal Bocagrande",
                    "Sucursal Granada",
                    "Sucursal San Fernando",
                    "Sucursal Centro Comercial",
                    "Sucursal Norte",
                    "Sucursal Principal",
                    "Sucursal Cabecera",
                    "Sucursal Centro"
                });
        }
    }
}
