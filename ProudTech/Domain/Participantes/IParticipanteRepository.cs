namespace ProudTech.Domain.Participantes
{
    public interface IParticipanteRepository
    {
        Task<Participante> ObterPorIdAsync(Guid participanteId);
    }
}
