namespace ProudTech.Domain.Trilhas
{
    public interface ITrilhaRepository
    {
        Task<bool> TrilhaFechadaAsync(Guid trilhaId, CancellationToken cancellationToken);
        Task InserirAsync(Trilha trilha, CancellationToken cancellationToken);
        Task<IEnumerable<Trilha>> ObterTodosAsync(CancellationToken cancellationToken);
        Task<Trilha> ObterPorIdAsync(Guid trilhaId, CancellationToken cancellationToken);
    }
}
