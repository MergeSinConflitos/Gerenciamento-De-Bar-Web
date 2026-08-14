using GerenciamentoDeBar.Dominio.Modulos.ModuloProprietario.cs;
using GerenciamentoDeBar.Infra.Compartilhado.Orm;
using GerenciamentoDeBar.Testes.E2E.Modulos.ModuloAutenticacao;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright.MSTest;

namespace GerenciamentoDeBar.Testes.E2E.Compartilhado;

public abstract class E2ETestsBase : PageTest
{
    private TestApplicationFactory aplicacao = null!;

    protected string UrlBase { get; private set; } = string.Empty;


    // =========================================================
    // INICIALIZAÇÃO
    // =========================================================

    [TestInitialize]
    public Task InicializarAplicacao()
    {
        aplicacao = new TestApplicationFactory();

        UrlBase = aplicacao.UrlBase;

        return Task.CompletedTask;
    }


    // =========================================================
    // ENCERRAMENTO
    // =========================================================

    [TestCleanup]
    public async Task EncerrarAplicacao()
    {
        try
        {
            if (aplicacao is not null)
                await aplicacao.DisposeAsync();
        }
        finally
        {
            aplicacao = null!;
        }
    }


    // =========================================================
    // REGISTRAR USUÁRIO
    // =========================================================

    protected async Task RegistrarUsuarioAsync(
        string email,
        string senha,
        string nome = "Proprietário Teste"
    )
    {
        using IServiceScope scope = aplicacao.Services.CreateScope();

        UserManager<IdentityUser<Guid>> userManager =
            scope.ServiceProvider
                .GetRequiredService<UserManager<IdentityUser<Guid>>>();

        GerenciamentoDeBarDbContext dbContext =
            scope.ServiceProvider
                .GetRequiredService<GerenciamentoDeBarDbContext>();


        IdentityUser<Guid> user = new()
        {
            Id = Guid.CreateVersion7(),
            UserName = email,
            Email = email
        };


        IdentityResult resultado =
            await userManager.CreateAsync(user, senha);


        Assert.IsTrue(
            resultado.Succeeded,
            string.Join(
                "; ",
                resultado.Errors.Select(erro => erro.Description)
            )
        );


        // Cria o proprietário associado ao usuário.

        Proprietario proprietario = new()
        {
            UserId = user.Id,
            Nome = nome
        };

        dbContext.proprietarios.Add(proprietario);

        await dbContext.SaveChangesAsync();
    }


    // =========================================================
    // REGISTRAR E ENTRAR
    // =========================================================

    protected async Task RegistrarEEntrarAsync(
        string email,
        string senha
    )
    {
        await RegistrarUsuarioAsync(email, senha);

        EntrarPage entrarPage =
            new(Page, UrlBase);

        await entrarPage.IrParaAsync();

        await entrarPage.PreencherAsync(
            email,
            senha
        );

        await entrarPage.ConfirmarAsync();
    }
}