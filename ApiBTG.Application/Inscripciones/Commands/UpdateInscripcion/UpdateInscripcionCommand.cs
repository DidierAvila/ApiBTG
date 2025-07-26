using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Inscripciones.Commands.UpdateInscripcion
{
    public record UpdateInscripcionCommand : IRequest<InscripcionDto>
    {
        public int Id { get; init; }
        public int IdCliente { get; init; }
        public int IdDisponibilidad { get; init; }
    }
} 