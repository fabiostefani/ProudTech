using ProudTech.Core;
using ProudTech.Domain.Participantes;
using ProudTech.Domain.Trilhas;

namespace ProudTech.Domain.Inscricoes
{
    public class Inscricao : EntityBase
    {
        public Guid ParticipanteId { get; set; }
        public Guid TrilhaId { get; set; }

        public virtual Participante Participante { get; set; }
        public virtual Trilha Trilha { get; set; }
    }
}
