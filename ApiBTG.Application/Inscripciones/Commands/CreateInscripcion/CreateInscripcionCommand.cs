using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Inscripciones.Commands.CreateInscripcion
{
    public record CreateInscripcionCommand : IRequest<InscripcionSimpleDto>
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdDisponibilidad { get; set; }
    }
} 