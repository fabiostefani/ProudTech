using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProudTech.Domain.Inscricoes;

namespace ProudTech.Data.Configs
{
    public class InscricaoConfig : IEntityTypeConfiguration<Inscricao>
    {
        public void Configure(EntityTypeBuilder<Inscricao> builder)
        {
            builder.ToTable(nameof(Inscricao));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ParticipanteId).IsRequired();
            builder.Property(x => x.TrilhaId).IsRequired();

            builder.HasOne(x => x.Participante)
            .WithMany()
            .HasForeignKey(x => x.ParticipanteId);

            builder.HasOne(x => x.Trilha)
            .WithMany()
            .HasForeignKey(x => x.TrilhaId);
        }
    }
}
