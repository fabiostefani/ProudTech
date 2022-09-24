using Microsoft.AspNetCore.Mvc;
using ProudTech.Domain.Trilhas;

namespace ProudTech.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrilhaController : ControllerBase
    {

        private readonly ILogger<TrilhaController> _logger;
        private readonly ITrilhaRepository trilhaRepository;

        public TrilhaController(ILogger<TrilhaController> logger,
                                ITrilhaRepository trilhaRepository)
        {
            _logger = logger;
            this.trilhaRepository = trilhaRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Trilha>> ObterTodos(CancellationToken cancellationToken)
        {
            return await this.trilhaRepository.ObterTodosAsync(cancellationToken);
        }

        [HttpGet("{id:guid}")]
        public async Task<Trilha> ObterPorId(Guid id, CancellationToken cancellationToken)
        {
            return await this.trilhaRepository.ObterPorIdAsync(id, cancellationToken);
        }

        [HttpPost]
        public async Task<Trilha> Inserir(Trilha trilha, CancellationToken cancellationToken)
        {
            await this.trilhaRepository.InserirAsync(trilha, cancellationToken);
            return trilha;
        }
    }
}