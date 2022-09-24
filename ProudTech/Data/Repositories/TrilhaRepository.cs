using Microsoft.EntityFrameworkCore;
using ProudTech.Domain.Trilhas;

namespace ProudTech.Data.Repositories
{
    public class TrilhaRepository : ITrilhaRepository
    {
        private readonly ProudTechContext context;

        public TrilhaRepository(ProudTechContext context)
        {
            this.context = context;
        }
        public async Task InserirAsync(Trilha trilha, CancellationToken cancellationToken)
        {
            trilha.Id = Guid.NewGuid();
            await this.context.Trilhas.AddAsync(trilha, cancellationToken);
            await this.context.SaveChangesAsync();
        }

        public async Task<bool> TrilhaFechadaAsync(Guid trilhaId, CancellationToken cancellationToken)
        {
            var trilha = await ObterPorIdAsync(trilhaId, cancellationToken);
            return trilha?.Fechada ?? false;
        }

        public async Task<IEnumerable<Trilha>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            return await this.context.Trilhas.ToListAsync(cancellationToken);
        }

        public async Task<Trilha> ObterPorIdAsync(Guid trilhaId, CancellationToken cancellationToken)
        {
            return await this.context.Trilhas.FirstOrDefaultAsync(x => x.Id == trilhaId, cancellationToken);
        }


    }
}
