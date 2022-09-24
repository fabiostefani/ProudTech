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

        public async Task RealizarInscricaoAsync(Inscricao inscricao, CancellationToken cancellationToken)
        {
            logger.LogWarning("Inicializando inscricao");

            if (await this.inscricaoRepository.ParticipanteInscritoAsync(inscricao.ParticipanteId, cancellationToken))
                throw new Exception("Participante já inscrito no ProudTech");

            if (await this.trilhaRepository.TrilhaFechadaAsync(inscricao.TrilhaId, cancellationToken))
                throw new Exception("Trilha já fechada");

            Participante participante = await this.participanteRepository.ObterPorIdAsync(inscricao.ParticipanteId, cancellationToken);
            participante.ValidarCadastroVerificado();

            await this.inscricaoRepository.InserirAsync(inscricao, cancellationToken);

            logger.LogWarning("Inscricao finalizada com êxito");
        }

        public async Task<IEnumerable<Inscricao>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            return await this.inscricaoRepository.ObterTodosAsync(cancellationToken);
        }
    }
}
