namespace ProudTech.Domain.Inscricoes
{
    public interface IInscricaoRepository
    {
        Task InserirAsync(Inscricao inscricao, CancellationToken cancellationToken);
        Task<bool> ParticipanteInscritoAsync(Guid participanteId, CancellationToken cancellationToken);
        Task<IEnumerable<Inscricao>> ObterTodosAsync(CancellationToken cancellationToken);
    }
}
