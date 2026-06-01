using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VagaJusta.Domain.Interfaces.Repositories;
using VagaJusta.Infrastructure.Data;
using VagaJusta.Infrastructure.Data.Repositories;

namespace VagaJusta.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBContext>(op => op.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<IEscolaRepository, EscolaRepository>();
            services.AddScoped<ITurmaRepository, TurmaRepository>();

            return services;
        }
    }
}
