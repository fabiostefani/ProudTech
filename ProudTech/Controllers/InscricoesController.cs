using Microsoft.AspNetCore.Mvc;
using ProudTech.Domain.Inscricoes;
using ProudTech.Services.Inscricoes;

namespace ProudTech.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InscricoesController : ControllerBase
    {

        private readonly ILogger<InscricoesController> _logger;
        private readonly IInscricaoService inscricaoService;

        public InscricoesController(ILogger<InscricoesController> logger,
                                IInscricaoService inscricaoService)
        {
            _logger = logger;
            this.inscricaoService = inscricaoService;
        }

        [HttpGet]
        public async Task<IEnumerable<Inscricao>> ObterTodos(CancellationToken cancellationToken)
        {
            return await this.inscricaoService.ObterTodosAsync(cancellationToken);
        }


        [HttpPost("realizar-inscricao")]
        public async Task<Inscricao> Inserir(Inscricao inscricao, CancellationToken cancellationToken)
        {
            await this.inscricaoService.RealizarInscricaoAsync(inscricao, cancellationToken);
            return inscricao;
        }
    }
}