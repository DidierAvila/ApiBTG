namespace ApiBTG.Domain.Dtos
{
    public class ClienteInscripcionDto
    {
        public int ClienteId { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public string ApellidosCliente { get; set; } = string.Empty;
        public string CiudadCliente { get; set; } = string.Empty;
        public int SucursalId { get; set; }
        public string NombreSucursal { get; set; } = string.Empty;
        public string CiudadSucursal { get; set; } = string.Empty;
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public string TipoProducto { get; set; } = string.Empty;
        public decimal MontoMinimo { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public DateTime UltimaVisita { get; set; }
    }
} 