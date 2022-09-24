using ProudTech.Core;

namespace ProudTech.Domain.Participantes
{
    public class Participante : EntityBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public bool CadastroVerificado { get; set; }

        public void ValidarCadastroVerificado()
        {
            if (CadastroVerificado) return;
            throw new Exception("Participante ainda não finalizou o cadastro.");
        }
    }
}
