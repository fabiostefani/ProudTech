using Microsoft.EntityFrameworkCore;
using ProudTech.Domain.Inscricoes;

namespace ProudTech.Data.Repositories
{
    public class InscricaoRepository : IInscricaoRepository
    {
        private readonly ProudTechContext context;

        public InscricaoRepository(ProudTechContext context)
        {
            this.context = context;
        }

        public async Task InserirAsync(Inscricao inscricao, CancellationToken cancellationToken)
        {
            inscricao.Id = Guid.NewGuid();
            await this.context.Inscricoes.AddAsync(inscricao, cancellationToken);
            await this.context.SaveChangesAsync();
        }

        public async Task<bool> ParticipanteInscritoAsync(Guid participanteId, CancellationToken cancellationToken)
        {
            return await this.context.Inscricoes.CountAsync(x => x.ParticipanteId == participanteId, cancellationToken) > 0;
        }


        public async Task<IEnumerable<Inscricao>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            return await this.context.Inscricoes.ToListAsync(cancellationToken);
        }
    }
}
