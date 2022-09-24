using Microsoft.AspNetCore.Mvc;
using ProudTech.Domain.Participantes;

namespace ProudTech.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParticipanteController : ControllerBase
    {

        private readonly ILogger<ParticipanteController> _logger;
        private readonly IParticipanteRepository participanteRepository;

        public ParticipanteController(ILogger<ParticipanteController> logger,
                                      IParticipanteRepository participanteRepository)
        {
            _logger = logger;
            this.participanteRepository = participanteRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Participante>> ObterTodos(CancellationToken cancellationToken)
        {
            return await this.participanteRepository.ObterTodosAsync(cancellationToken);
        }

        [HttpGet("{id:guid}")]
        public async Task<Participante> ObterPorId(Guid id, CancellationToken cancellationToken)
        {
            return await this.participanteRepository.ObterPorIdAsync(id, cancellationToken);
        }

        [HttpPost]
        public async Task<Participante> Inserir(Participante participante, CancellationToken cancellationToken)
        {
            await this.participanteRepository.InserirAsync(participante, cancellationToken);
            return participante;
        }
    }
}