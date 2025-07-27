using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiBTG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InsertInitialUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [Usuario] ON;
                INSERT INTO [Usuario] ([Id], [Nombre], [Apellido], [Email], [Clave], [Rol], [NotificacionPreferida], [Telefono])
                VALUES 
                (1, 'Alejo', 'Pertuz', 'alejopertuz@gmail.com', '123', 'administrador', 'SMS', '+573218899857'),
                (2, 'Ana', 'Frank', 'anafrank@gmail.com', '321', 'usuario', 'Email', '+578899966455');
                SET IDENTITY_INSERT [Usuario] OFF;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM [Usuario] WHERE [Id] IN (1, 2);
            ");
        }
    }
}
