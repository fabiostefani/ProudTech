using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ProudTech.Domain.Inscricoes;
using ProudTech.Domain.Participantes;
using ProudTech.Domain.Trilhas;
using ProudTech.Services.Inscricoes;

namespace ProudTech.Tests.Services.Inscricoes
{
    public class InscricaoServiceTestMock
    {
        private readonly Participante Participante;
        public InscricaoServiceTestMock()
        {
            Participante = ConstruirParticipante();
        }

        [Fact]
        public async Task RealizarInscricao_ExecutarComSucesso_SeParticipanteAindaNaoIncrito()
        {
            var inscricao = ConstruirInscricao();

            var inscricaoRepositoryMock = new Mock<IInscricaoRepository>();
            var participanteRepositoryMock = new Mock<IParticipanteRepository>();
            var trilhaRepositoryMock = new Mock<ITrilhaRepository>();
            var loggerMock = new Mock<ILogger<InscricaoService>>();
            participanteRepositoryMock.Setup(x => x.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Participante);

            var inscricaoService = new InscricaoService(loggerMock.Object, 
                                                        inscricaoRepositoryMock.Object, 
                                                        participanteRepositoryMock.Object, 
                                                        trilhaRepositoryMock.Object);

            await inscricaoService.RealizarInscricaoAsync(inscricao, CancellationToken.None);

            inscricaoRepositoryMock.Verify(x => x.InserirAsync(It.IsAny<Inscricao>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void RealizarInscricao_GerarException_SeParticipanteJaEstaIncrito()
        {
            Inscricao inscricao = ConstruirInscricao();

            var inscricaoRepositoryMock = new Mock<IInscricaoRepository>();
            var participanteRepositoryMock = new Mock<IParticipanteRepository>();
            var trilhaRepositoryMock = new Mock<ITrilhaRepository>();
            var loggerMock = new Mock<ILogger<InscricaoService>>();
            inscricaoRepositoryMock.Setup(x => x.ParticipanteInscritoAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var inscricaoService = new InscricaoService(loggerMock.Object, inscricaoRepositoryMock.Object, participanteRepositoryMock.Object, trilhaRepositoryMock.Object);

            var ex = Assert.ThrowsAsync<Exception>(async () => await inscricaoService.RealizarInscricaoAsync(inscricao, CancellationToken.None)).Result;
            Assert.Equal("Participante já inscrito no ProudTech", ex.Message);
        }

        [Fact]
        public void RealizarInscricao_GerarException_SeParticipanteJaEstaIncritoFluentAssertions()
        {
            Inscricao inscricao = ConstruirInscricao();

            var inscricaoRepositoryMock = new Mock<IInscricaoRepository>();
            var participanteRepositoryMock = new Mock<IParticipanteRepository>();
            var trilhaRepositoryMock = new Mock<ITrilhaRepository>();
            var loggerMock = new Mock<ILogger<InscricaoService>>();
            inscricaoRepositoryMock.Setup(x => x.ParticipanteInscritoAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var inscricaoService = new InscricaoService(loggerMock.Object, inscricaoRepositoryMock.Object, participanteRepositoryMock.Object, trilhaRepositoryMock.Object);

            var ex = Assert.ThrowsAsync<Exception>(async () => await inscricaoService.RealizarInscricaoAsync(inscricao, CancellationToken.None)).Result;
            ex.Message.Should().Be("Participante já inscrito no ProudTech");
        }

        [Fact]
        public void RealizarInscricao_GerarException_SeParticipanteAindaNaoFinalizouCadastro()
        {
            Inscricao inscricao = ConstruirInscricao();

            var inscricaoRepositoryMock = new Mock<IInscricaoRepository>();
            var participanteRepositoryMock = new Mock<IParticipanteRepository>();
            var trilhaRepositoryMock = new Mock<ITrilhaRepository>();
            var loggerMock = new Mock<ILogger<InscricaoService>>();
            Participante.CadastroVerificado = false;

            inscricaoRepositoryMock.Setup(x => x.ParticipanteInscritoAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
            participanteRepositoryMock.Setup(x => x.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(Participante);

            var inscricaoService = new InscricaoService(loggerMock.Object, inscricaoRepositoryMock.Object, participanteRepositoryMock.Object, trilhaRepositoryMock.Object);

            var ex = Assert.ThrowsAsync<Exception>(async () => await inscricaoService.RealizarInscricaoAsync(inscricao, CancellationToken.None)).Result;
            ex.Message.Should().Be("Participante ainda não finalizou o cadastro.");
            Assert.Equal("Participante ainda não finalizou o cadastro.", ex.Message);
        }

        [Fact]
        public void RealizarInscricao_GerarException_SeTrilhaJaFechada()
        {
            Inscricao inscricao = ConstruirInscricao();

            var inscricaoRepositoryMock = new Mock<IInscricaoRepository>();
            var participanteRepositoryMock = new Mock<IParticipanteRepository>();
            var trilhaRepositoryMock = new Mock<ITrilhaRepository>();
            var loggerMock = new Mock<ILogger<InscricaoService>>();
            Participante.CadastroVerificado = false;

            inscricaoRepositoryMock.Setup(x => x.ParticipanteInscritoAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
            participanteRepositoryMock.Setup(x => x.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(Participante);
            trilhaRepositoryMock.Setup(x => x.TrilhaFechadaAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var inscricaoService = new InscricaoService(loggerMock.Object, inscricaoRepositoryMock.Object, participanteRepositoryMock.Object, trilhaRepositoryMock.Object);

            var ex = Assert.ThrowsAsync<Exception>(async () => await inscricaoService.RealizarInscricaoAsync(inscricao, CancellationToken.None)).Result;
            ex.Message.Should().Be("Trilha já fechada");
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
