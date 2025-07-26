using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Inscripciones.Queries.GetInscripcionById
{
    public class GetInscripcionByIdQuery : IRequest<InscripcionDto?>
    {
        public int Id { get; set; }
    }
} 