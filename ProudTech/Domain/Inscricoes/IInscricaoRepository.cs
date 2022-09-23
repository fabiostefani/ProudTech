namespace ProudTech.Domain.Inscricoes
{
    public interface IInscricaoRepository
    {
        Task InserirAsync(Inscricao inscricao);
        Task<bool> ParticipanteInscritoAsync(Guid participanteId);
    }
}
