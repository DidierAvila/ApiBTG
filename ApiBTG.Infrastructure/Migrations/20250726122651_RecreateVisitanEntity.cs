using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBTG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RecreateVisitanEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disponibilidades_Productos_IdProducto",
                table: "Disponibilidades");

            migrationBuilder.DropForeignKey(
                name: "FK_Disponibilidades_Sucursales_IdSucursal",
                table: "Disponibilidades");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscripciones_Clientes_IdCliente",
                table: "Inscripciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscripciones_Disponibilidades_IdDisponibilidad",
                table: "Inscripciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscripciones_Productos_ProductoId",
                table: "Inscripciones");

            migrationBuilder.DropTable(
                name: "Visitas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sucursales",
                table: "Sucursales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Productos",
                table: "Productos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inscripciones",
                table: "Inscripciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Disponibilidades",
                table: "Disponibilidades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.RenameTable(
                name: "Sucursales",
                newName: "Sucursal");

            migrationBuilder.RenameTable(
                name: "Productos",
                newName: "Producto");

            migrationBuilder.RenameTable(
                name: "Inscripciones",
                newName: "Inscripcion");

            migrationBuilder.RenameTable(
                name: "Disponibilidades",
                newName: "Disponibilidad");

            migrationBuilder.RenameTable(
                name: "Clientes",
                newName: "Cliente");

            migrationBuilder.RenameIndex(
                name: "IX_Inscripciones_ProductoId",
                table: "Inscripcion",
                newName: "IX_Inscripcion_ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_Inscripciones_IdDisponibilidad",
                table: "Inscripcion",
                newName: "IX_Inscripcion_IdDisponibilidad");

            migrationBuilder.RenameIndex(
                name: "IX_Inscripciones_IdCliente_IdDisponibilidad",
                table: "Inscripcion",
                newName: "IX_Inscripcion_IdCliente_IdDisponibilidad");

            migrationBuilder.RenameIndex(
                name: "IX_Disponibilidades_IdSucursal_IdProducto",
                table: "Disponibilidad",
                newName: "IX_Disponibilidad_IdSucursal_IdProducto");

            migrationBuilder.RenameIndex(
                name: "IX_Disponibilidades_IdProducto",
                table: "Disponibilidad",
                newName: "IX_Disponibilidad_IdProducto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sucursal",
                table: "Sucursal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Producto",
                table: "Producto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inscripcion",
                table: "Inscripcion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Disponibilidad",
                table: "Disponibilidad",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Visita",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSucursal = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    FechaVisita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoAccion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visita", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visita_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visita_Sucursal_IdSucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visita_IdCliente",
                table: "Visita",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Visita_IdSucursal",
                table: "Visita",
                column: "IdSucursal");

            migrationBuilder.AddForeignKey(
                name: "FK_Disponibilidad_Producto_IdProducto",
                table: "Disponibilidad",
                column: "IdProducto",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Disponibilidad_Sucursal_IdSucursal",
                table: "Disponibilidad",
                column: "IdSucursal",
                principalTable: "Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscripcion_Cliente_IdCliente",
                table: "Inscripcion",
                column: "IdCliente",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscripcion_Disponibilidad_IdDisponibilidad",
                table: "Inscripcion",
                column: "IdDisponibilidad",
                principalTable: "Disponibilidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscripcion_Producto_ProductoId",
                table: "Inscripcion",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disponibilidad_Producto_IdProducto",
                table: "Disponibilidad");

            migrationBuilder.DropForeignKey(
                name: "FK_Disponibilidad_Sucursal_IdSucursal",
                table: "Disponibilidad");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscripcion_Cliente_IdCliente",
                table: "Inscripcion");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscripcion_Disponibilidad_IdDisponibilidad",
                table: "Inscripcion");

            migrationBuilder.DropForeignKey(
                name: "FK_Inscripcion_Producto_ProductoId",
                table: "Inscripcion");

            migrationBuilder.DropTable(
                name: "Visita");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sucursal",
                table: "Sucursal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Producto",
                table: "Producto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inscripcion",
                table: "Inscripcion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Disponibilidad",
                table: "Disponibilidad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.RenameTable(
                name: "Sucursal",
                newName: "Sucursales");

            migrationBuilder.RenameTable(
                name: "Producto",
                newName: "Productos");

            migrationBuilder.RenameTable(
                name: "Inscripcion",
                newName: "Inscripciones");

            migrationBuilder.RenameTable(
                name: "Disponibilidad",
                newName: "Disponibilidades");

            migrationBuilder.RenameTable(
                name: "Cliente",
                newName: "Clientes");

            migrationBuilder.RenameIndex(
                name: "IX_Inscripcion_ProductoId",
                table: "Inscripciones",
                newName: "IX_Inscripciones_ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_Inscripcion_IdDisponibilidad",
                table: "Inscripciones",
                newName: "IX_Inscripciones_IdDisponibilidad");

            migrationBuilder.RenameIndex(
                name: "IX_Inscripcion_IdCliente_IdDisponibilidad",
                table: "Inscripciones",
                newName: "IX_Inscripciones_IdCliente_IdDisponibilidad");

            migrationBuilder.RenameIndex(
                name: "IX_Disponibilidad_IdSucursal_IdProducto",
                table: "Disponibilidades",
                newName: "IX_Disponibilidades_IdSucursal_IdProducto");

            migrationBuilder.RenameIndex(
                name: "IX_Disponibilidad_IdProducto",
                table: "Disponibilidades",
                newName: "IX_Disponibilidades_IdProducto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sucursales",
                table: "Sucursales",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Productos",
                table: "Productos",
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

            migrationBuilder.CreateTable(
                name: "Visitas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdSucursal = table.Column<int>(type: "int", nullable: false),
                    FechaVisita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoAccion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visitas_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visitas_Sucursales_IdSucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_IdCliente",
                table: "Visitas",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Visitas_IdSucursal",
                table: "Visitas",
                column: "IdSucursal");

            migrationBuilder.AddForeignKey(
                name: "FK_Disponibilidades_Productos_IdProducto",
                table: "Disponibilidades",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Disponibilidades_Sucursales_IdSucursal",
                table: "Disponibilidades",
                column: "IdSucursal",
                principalTable: "Sucursales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscripciones_Clientes_IdCliente",
                table: "Inscripciones",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
