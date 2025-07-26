using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBTG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProductoIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the foreign key constraint for ProductoId
            migrationBuilder.DropForeignKey(
                name: "FK_Inscripciones_Productos_ProductoId",
                table: "Inscripciones");

            // Drop the index for ProductoId
            migrationBuilder.DropIndex(
                name: "IX_Inscripciones_ProductoId",
                table: "Inscripciones");

            // Drop the ProductoId column
            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Inscripciones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Re-add the ProductoId column
            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "Inscripciones",
                type: "int",
                nullable: true);

            // Re-create the index for ProductoId
            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_ProductoId",
                table: "Inscripciones",
                column: "ProductoId");

            // Re-add the foreign key constraint for ProductoId
            migrationBuilder.AddForeignKey(
                name: "FK_Inscripciones_Productos_ProductoId",
                table: "Inscripciones",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id");
        }
    }
}
