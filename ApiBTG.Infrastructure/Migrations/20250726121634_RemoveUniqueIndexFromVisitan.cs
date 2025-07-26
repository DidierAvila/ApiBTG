using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBTG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueIndexFromVisitan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Visitas_IdSucursal_IdCliente",
                table: "Visitas");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_IdSucursal",
                table: "Visitas",
                column: "IdSucursal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Visitas_IdSucursal",
                table: "Visitas");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_IdSucursal_IdCliente",
                table: "Visitas",
                columns: new[] { "IdSucursal", "IdCliente" },
                unique: true);
        }
    }
}
