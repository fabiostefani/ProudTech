namespace ProudTech.Domain.Trilhas
{
    public interface ITrilhaRepository
    {
        Task<bool> TrilhaFechadaAsync(Guid trilhaId);
    }
}
