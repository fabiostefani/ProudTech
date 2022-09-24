using Microsoft.EntityFrameworkCore;
using ProudTech.Domain.Participantes;

namespace ProudTech.Data.Repositories
{
    public class ParticipanteRepository : IParticipanteRepository
    {
        private readonly ProudTechContext context;

        public ParticipanteRepository(ProudTechContext context)
        {
            this.context = context;
        }

        public async Task InserirAsync(Participante participante, CancellationToken cancellationToken)
        {
            participante.Id = Guid.NewGuid();
            await this.context.Participantes.AddAsync(participante, cancellationToken);
            await this.context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Participante>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            return await this.context.Participantes.ToListAsync(cancellationToken);
        }

        public async Task<Participante> ObterPorIdAsync(Guid participanteId, CancellationToken cancellationToken)
        {
            return await this.context.Participantes.FirstOrDefaultAsync(x => x.Id == participanteId, cancellationToken);
        }
    }
}
