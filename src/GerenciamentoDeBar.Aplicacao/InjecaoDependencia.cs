using GerenciamentoDeBar.Aplicacao.Modulos.ModuloMesa;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciamentoDeBar.Aplicacao;

public static class InjecaoDependencia
{
    public static void AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // Módulo de Mesas
        services.AddScoped<ServicoMesa>();

        // Módulo de Garçons
        // services.AddScoped<ServicoGarcom>();

        // Módulo de Produtos
        // services.AddScoped<ServicoProduto>();

        // Módulo de Contas
        // services.AddScoped<ServicoConta>();

        // Módulo de Itens de Pedido
        // services.AddScoped<ServicoItemPedido>();
    }
}