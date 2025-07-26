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
            migrationBuilder.InsertData(
                table: "Sucursal",
                columns: new[] { "Id", "Nombre", "Ciudad" },
                values: new object[,]
                {
                    { 1, "Sucursal Centro", "Bogotá" },
                    { 2, "Sucursal Norte", "Medellín" },
                    { 3, "Sucursal Sur", "Cali" },
                    { 4, "Sucursal Este", "Barranquilla" },
                    { 5, "Sucursal Oeste", "Cartagena" },
                    { 6, "Sucursal Plaza Central", "Bucaramanga" },
                    { 7, "Sucursal Mall del Norte", "Pereira" },
                    { 8, "Sucursal Centro Comercial", "Manizales" },
                    { 9, "Sucursal Plaza Mayor", "Villavicencio" },
                    { 10, "Sucursal Terminal", "Pasto" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sucursal",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9, 10
                });
        }
    }
}
