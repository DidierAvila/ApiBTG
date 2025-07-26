using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBTG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameIdProductoToIdDisponibilidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscripciones_Productos_IdProducto",
                table: "Inscripciones");

            migrationBuilder.DropIndex(
                name: "IX_Inscripciones_IdCliente",
                table: "Inscripciones");

            migrationBuilder.DropIndex(
                name: "IX_Inscripciones_IdProducto_IdCliente",
                table: "Inscripciones");

            migrationBuilder.RenameColumn(
                name: "IdProducto",
                table: "Inscripciones",
                newName: "IdDisponibilidad");

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "Inscripciones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_IdCliente_IdDisponibilidad",
                table: "Inscripciones",
                columns: new[] { "IdCliente", "IdDisponibilidad" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_IdDisponibilidad",
                table: "Inscripciones",
                column: "IdDisponibilidad");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_ProductoId",
                table: "Inscripciones",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inscripciones_Disponibilidades_IdDisponibilidad",
                table: "Inscripciones",
                column: "IdDisponibilidad",
                principalTable: "Disponibilidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscripciones_Productos_ProductoId",
                table: "Inscripciones",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscripciones_Disponibilidades_IdDisponibilidad",
                table: "Inscripciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscripciones_Productos_ProductoId",
                table: "Inscripciones");

            migrationBuilder.DropIndex(
                name: "IX_Inscripciones_IdCliente_IdDisponibilidad",
                table: "Inscripciones");

            migrationBuilder.DropIndex(
                name: "IX_Inscripciones_IdDisponibilidad",
                table: "Inscripciones");

            migrationBuilder.DropIndex(
                name: "IX_Inscripciones_ProductoId",
                table: "Inscripciones");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Inscripciones");

            migrationBuilder.RenameColumn(
                name: "IdDisponibilidad",
                table: "Inscripciones",
                newName: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_IdCliente",
                table: "Inscripciones",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_IdProducto_IdCliente",
                table: "Inscripciones",
                columns: new[] { "IdProducto", "IdCliente" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscripciones_Productos_IdProducto",
                table: "Inscripciones",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
