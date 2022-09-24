using ProudTech.Core;

namespace ProudTech.Domain.Trilhas
{
    public class Trilha : EntityBase
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Fechada { get; set; }

    }
}
