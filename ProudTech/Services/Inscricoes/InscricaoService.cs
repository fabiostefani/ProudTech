using ProudTech.Domain.Inscricoes;
using ProudTech.Domain.Participantes;
using ProudTech.Domain.Trilhas;

namespace ProudTech.Services.Inscricoes
{
    public class InscricaoService : IInscricaoService
    {
        private readonly ILogger<InscricaoService> logger;
        private readonly IInscricaoRepository inscricaoRepository;
        private readonly IParticipanteRepository participanteRepository;
        private readonly ITrilhaRepository trilhaRepository;

        public InscricaoService(ILogger<InscricaoService> logger,
                                IInscricaoRepository inscricaoRepository,
                                IParticipanteRepository participanteRepository,
                                ITrilhaRepository trilhaRepository)
        {
            this.logger = logger;
            this.inscricaoRepository = inscricaoRepository;
            this.participanteRepository = participanteRepository;
            this.trilhaRepository = trilhaRepository;
        }

        public async Task RealizarInscricaoAsync(Inscricao inscricao)
        {
            logger.LogWarning("Inicializando inscricao");

            if (await this.inscricaoRepository.ParticipanteInscritoAsync(inscricao.ParticipanteId))
                throw new Exception("Participante já inscrito no ProudTech");

            if (await this.trilhaRepository.TrilhaFechadaAsync(inscricao.TrilhaId))
                throw new Exception("Trilha já fechada");

            Participante participante = await this.participanteRepository.ObterPorIdAsync(inscricao.ParticipanteId);
            participante.ValidarCadastroVerificado();

            await this.inscricaoRepository.InserirAsync(inscricao);

            logger.LogWarning("Inscricao finalizada com êxito");
        }
    }
}
