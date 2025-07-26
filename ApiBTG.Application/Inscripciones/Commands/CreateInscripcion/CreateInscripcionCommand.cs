using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Inscripciones.Commands.CreateInscripcion
{
    public record CreateInscripcionCommand : IRequest<InscripcionDto>
    {
        public int IdCliente { get; init; }
        public int IdDisponibilidad { get; init; }
    }
} 