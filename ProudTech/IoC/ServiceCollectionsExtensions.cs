using Microsoft.EntityFrameworkCore;
using ProudTech.Data;
using ProudTech.Data.Repositories;
using ProudTech.Domain.Inscricoes;
using ProudTech.Domain.Participantes;
using ProudTech.Domain.Trilhas;
using ProudTech.Services.Inscricoes;

namespace ProudTech.IoC
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProudTechContext>(dbContextOptions
                => dbContextOptions.UseLazyLoadingProxies()
                    .UseSqlite(configuration.GetConnectionString("DefaultConnection"), sqliteOptions
                        => sqliteOptions.MigrationsAssembly(typeof(ProudTechContext).Assembly.GetName().Name)));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITrilhaRepository, TrilhaRepository>();
            services.AddScoped<IParticipanteRepository, ParticipanteRepository>();
            services.AddScoped<IInscricaoRepository, InscricaoRepository>();
            return services;
        }

        public static IServiceCollection AddServicos(this IServiceCollection services)
        {
            services.AddScoped<IInscricaoService, InscricaoService>();
            return services;
        }
    }
}
