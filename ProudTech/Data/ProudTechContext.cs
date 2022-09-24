using Microsoft.EntityFrameworkCore;
using ProudTech.Domain.Inscricoes;
using ProudTech.Domain.Participantes;
using ProudTech.Domain.Trilhas;

namespace ProudTech.Data
{
    public class ProudTechContext : DbContext
    {
        public ProudTechContext(DbContextOptions<ProudTechContext> options)
            : base(options)
        {

        }

        public DbSet<Trilha> Trilhas { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Inscricao> Inscricoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProudTechContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
