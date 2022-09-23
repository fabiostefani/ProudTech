using ProudTech.Core;

namespace ProudTech.Domain.Inscricoes
{
    public class Inscricao : EntityBase
    {
        public Guid ParticipanteId { get; set; }
        public Guid TrilhaId { get; set; }
    }
}
