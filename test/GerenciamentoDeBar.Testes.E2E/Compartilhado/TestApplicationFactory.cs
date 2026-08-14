using GerenciamentoDeBar.Infra.Compartilhado.Orm;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Playwright;

namespace GerenciamentoDeBar.Testes.E2E.Compartilhado;

public sealed class TestApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string nomeBanco;

    public string UrlBase { get; }

    public TestApplicationFactory()
    {
        nomeBanco = $"e2e-{Guid.NewGuid():N}";

        UseKestrel(0);

        StartServer();

        UrlBase = ObterUrlKestrel();
    }


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // Remove a configuração do DbContext utilizada
            // pela aplicação normalmente.
            services.RemoveAll<
                DbContextOptions<GerenciamentoDeBarDbContext>
            >();

            services.RemoveAll<
                IDbContextOptionsConfiguration<GerenciamentoDeBarDbContext>
            >();


            // Adiciona um banco InMemory exclusivo para este teste.
            services.AddDbContext<GerenciamentoDeBarDbContext>(
                options =>
                {
                    options.UseInMemoryDatabase(nomeBanco);
                }
            );
        });
    }


    private string ObterUrlKestrel()
    {
        IServer servidor =
            Services.GetRequiredService<IServer>();

        IServerAddressesFeature? enderecos =
            servidor.Features.Get<IServerAddressesFeature>();

        if (enderecos is null)
        {
            throw new InvalidOperationException(
                "Não foi possível obter a URL do servidor."
            );
        }

        return enderecos.Addresses.Single();
    }
}