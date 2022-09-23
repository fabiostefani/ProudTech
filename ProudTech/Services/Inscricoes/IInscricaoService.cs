using ProudTech.Domain.Inscricoes;

namespace ProudTech.Services.Inscricoes
{
    public interface IInscricaoService
    {
        Task RealizarInscricaoAsync(Inscricao inscricao);
    }
}
