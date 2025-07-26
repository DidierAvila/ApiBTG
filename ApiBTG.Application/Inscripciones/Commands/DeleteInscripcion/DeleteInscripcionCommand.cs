using MediatR;

namespace ApiBTG.Application.Inscripciones.Commands.DeleteInscripcion
{
    public class DeleteInscripcionCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
} 