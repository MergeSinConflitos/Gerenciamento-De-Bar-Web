using GerenciamentoDeBar.Dominio.Modulos.ModuloMesa;
using GerenciamentoDeBar.Infra.Compartilhado.Logging;
using GerenciamentoDeBar.Infra.Compartilhado.Orm;
using GerenciamentoDeBar.Infra.Modulos.ModuloMesa;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace GerenciamentoDeBar.Infra.Compartilhado;

public static class InjecaoDependencia
{
    public static void AddInfraRepositories(
    this IServiceCollection services,
    IConfiguration configuration,
    ILoggingBuilder logging,
    IHostEnvironment environment
)
    {
        Log.Logger = SerilogFactory.Create(
            configuration,
            environment);

        logging.ClearProviders();

        services.AddSerilog(Log.Logger);

        // Injeta o DbContext do EF
        services.AddDbContext<GerenciamentoDeBarDbContext>(options =>
        {
            string? connectionString = configuration.GetConnectionString("SqlServerEF");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    $"A connection string \"SqlServerEF\" não foi encontrada."
                );
            }

            options.UseSqlServer(connectionString, opt =>
            {
                opt.EnableRetryOnFailure(3);
            });
        });

        // Configuração do Usuário no Identity
        services.AddIdentityCore<IdentityUser<Guid>>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
        })
        .AddRoles<IdentityRole<Guid>>() // Configuração de Cargos/Papéis no Identity
        .AddEntityFrameworkStores<GerenciamentoDeBarDbContext>() // Integração com EntityFramework
        .AddSignInManager()
        .AddDefaultTokenProviders();

        // Use de exemplo a medida que cria os modulos 
        services.AddScoped<IRepositorioMesa, RepositorioMesaEmOrm>();


    }
}