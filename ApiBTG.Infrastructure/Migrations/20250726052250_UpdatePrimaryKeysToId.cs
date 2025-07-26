using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBTG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePrimaryKeysToId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscripciones_Cliente_IdCliente",
                table: "Inscripciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitas_Cliente_IdCliente",
                table: "Visitas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visitas",
                table: "Visitas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inscripciones",
                table: "Inscripciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Disponibilidades",
                table: "Disponibilidades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.RenameTable(
                name: "Cliente",
                newName: "Clientes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Visitas",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Inscripciones",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Disponibilidades",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visitas",
                table: "Visitas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inscripciones",
                table: "Inscripciones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Disponibilidades",
                table: "Disponibilidades",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_IdSucursal_IdCliente",
                table: "Visitas",
                columns: new[] { "IdSucursal", "IdCliente" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_IdProducto_IdCliente",
                table: "Inscripciones",
                columns: new[] { "IdProducto", "IdCliente" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Disponibilidades_IdSucursal_IdProducto",
                table: "Disponibilidades",
                columns: new[] { "IdSucursal", "IdProducto" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscripciones_Clientes_IdCliente",
                table: "Inscripciones",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitas_Clientes_IdCliente",
                table: "Visitas",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscripciones_Clientes_IdCliente",
                table: "Inscripciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitas_Clientes_IdCliente",
                table: "Visitas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visitas",
                table: "Visitas");

            migrationBuilder.DropIndex(
                name: "IX_Visitas_IdSucursal_IdCliente",
                table: "Visitas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inscripciones",
                table: "Inscripciones");

            migrationBuilder.DropIndex(
                name: "IX_Inscripciones_IdProducto_IdCliente",
                table: "Inscripciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Disponibilidades",
                table: "Disponibilidades");

            migrationBuilder.DropIndex(
                name: "IX_Disponibilidades_IdSucursal_IdProducto",
                table: "Disponibilidades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Visitas");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Inscripciones");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Disponibilidades");

            migrationBuilder.RenameTable(
                name: "Clientes",
                newName: "Cliente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visitas",
                table: "Visitas",
                columns: new[] { "IdSucursal", "IdCliente" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inscripciones",
                table: "Inscripciones",
                columns: new[] { "IdProducto", "IdCliente" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Disponibilidades",
                table: "Disponibilidades",
                columns: new[] { "IdSucursal", "IdProducto" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inscripciones_Cliente_IdCliente",
                table: "Inscripciones",
                column: "IdCliente",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitas_Cliente_IdCliente",
                table: "Visitas",
                column: "IdCliente",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
