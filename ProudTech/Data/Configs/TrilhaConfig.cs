using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProudTech.Domain.Trilhas;

namespace ProudTech.Data.Configs
{
    public class TrilhaConfig : IEntityTypeConfiguration<Trilha>
    {
        public void Configure(EntityTypeBuilder<Trilha> builder)
        {
            builder.ToTable(nameof(Trilha));
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Descricao).IsRequired();
            builder.Property(x => x.Nome).IsRequired();

        }
    }
}
