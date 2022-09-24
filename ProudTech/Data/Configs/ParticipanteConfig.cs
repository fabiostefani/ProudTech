using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProudTech.Domain.Participantes;

namespace ProudTech.Data.Configs
{
    public class ParticipanteConfig : IEntityTypeConfiguration<Participante>
    {
        public void Configure(EntityTypeBuilder<Participante> builder)
        {
            builder.ToTable(nameof(Participante));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CadastroVerificado).IsRequired();
            builder.Property(x => x.Telefone).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Nome).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(200).IsRequired();
        }
    }
}
