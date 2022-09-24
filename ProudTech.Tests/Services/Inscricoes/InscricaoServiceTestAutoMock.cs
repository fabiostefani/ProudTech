using Moq;
using Moq.AutoMock;
using ProudTech.Domain.Inscricoes;
using ProudTech.Domain.Participantes;
using ProudTech.Domain.Trilhas;
using ProudTech.Services.Inscricoes;

namespace ProudTech.Tests.Services.Inscricoes
{
    public class InscricaoServiceTestAutoMock
    {
        private readonly AutoMocker AutoMoq;
        private readonly Participante Participante;
        public InscricaoServiceTestAutoMock()
        {
            AutoMoq = new AutoMocker();
            Participante = ConstruirParticipante();
        }
        [Fact]
        public async Task RealizarInscricao_ExecutarComSucesso_SeParticipanteAindaNaoIncrito()
        {
            Inscricao inscricao = ConstruirInscricao();

            var inscricaoRepositoryMock = AutoMoq.GetMock<IInscricaoRepository>();
            var participanteRepositoryMock = AutoMoq.GetMock<IParticipanteRepository>();

            participanteRepositoryMock.Setup(x => x.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(Participante);

            var inscricaoService = AutoMoq.CreateInstance<InscricaoService>();

            await inscricaoService.RealizarInscricaoAsync(inscricao, CancellationToken.None);

            inscricaoRepositoryMock.Verify(x => x.InserirAsync(It.IsAny<Inscricao>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void RealizarInscricao_GerarException_SeParticipanteJaEstaIncrito()
        {
            Inscricao inscricao = ConstruirInscricao();

            var inscricaoRepositoryMock = AutoMoq.GetMock<IInscricaoRepository>();
            inscricaoRepositoryMock.Setup(x => x.ParticipanteInscritoAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var inscricaoService = AutoMoq.CreateInstance<InscricaoService>();

            var ex = Assert.ThrowsAsync<Exception>(async () => await inscricaoService.RealizarInscricaoAsync(inscricao, CancellationToken.None)).Result;
            Assert.Equal("Participante já inscrito no ProudTech", ex.Message);
        }

        [Fact]
        public void RealizarInscricao_GerarException_SeParticipanteAindaNaoFinalizouCadastro()
        {
            Inscricao inscricao = ConstruirInscricao();

            var inscricaoRepositoryMock = AutoMoq.GetMock<IInscricaoRepository>();
            var participanteRepositoryMock = AutoMoq.GetMock<IParticipanteRepository>();

            Participante.CadastroVerificado = false;

            inscricaoRepositoryMock.Setup(x => x.ParticipanteInscritoAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
            participanteRepositoryMock.Setup(x => x.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(Participante);

            var inscricaoService = AutoMoq.CreateInstance<InscricaoService>();

            var ex = Assert.ThrowsAsync<Exception>(async () => await inscricaoService.RealizarInscricaoAsync(inscricao, CancellationToken.None)).Result;
            Assert.Equal("Participante ainda não finalizou o cadastro.", ex.Message);
        }

        [Fact]
        public void RealizarInscricao_GerarException_SeTrilhaJaFechada()
        {
            Inscricao inscricao = ConstruirInscricao();

            var inscricaoRepositoryMock = AutoMoq.GetMock<IInscricaoRepository>();
            var participanteRepositoryMock = AutoMoq.GetMock<IParticipanteRepository>();
            var trilhaRepositoryMock = AutoMoq.GetMock<ITrilhaRepository>();

            Participante.CadastroVerificado = false;

            inscricaoRepositoryMock.Setup(x => x.ParticipanteInscritoAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
            participanteRepositoryMock.Setup(x => x.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(Participante);
            trilhaRepositoryMock.Setup(x => x.TrilhaFechadaAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var inscricaoService = AutoMoq.CreateInstance<InscricaoService>();

            var ex = Assert.ThrowsAsync<Exception>(async () => await inscricaoService.RealizarInscricaoAsync(inscricao, CancellationToken.None)).Result;
            Assert.Equal("Trilha já fechada", ex.Message);
        }

        private static Participante ConstruirParticipante()
            => new() { CadastroVerificado = true };

        private static Inscricao ConstruirInscricao()
        {
            return new Inscricao()
            {
                ParticipanteId = Guid.NewGuid(),
                TrilhaId = Guid.NewGuid()
            };
        }
    }
}