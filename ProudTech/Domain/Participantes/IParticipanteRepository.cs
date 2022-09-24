namespace ProudTech.Domain.Participantes
{
    public interface IParticipanteRepository
    {
        Task InserirAsync(Participante participante, CancellationToken cancellationToken);
        Task<IEnumerable<Participante>> ObterTodosAsync(CancellationToken cancellationToken);
        Task<Participante> ObterPorIdAsync(Guid participanteId, CancellationToken cancellationToken);
    }
}
