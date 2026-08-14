using GerenciamentoDeBar.Testes.E2E.Compartilhado;
using GerenciamentoDeBar.Testes.E2E.Modulos.ModuloHome;

namespace GerenciamentoDeBar.Testes.E2E.Modulos.ModuloAutenticacao;

[TestClass]
public sealed class AutenticacaoE2ETests : E2ETestsBase
{
    [TestMethod]
    public async Task Deve_PermitirAcessoAPaginaInicial_ParaUsuarioAnonimo()
    {
        // Arrange
        HomePage homePage = new(Page, UrlBase);

        // Act
        await homePage.IrParaAsync();

        // Assert
        await Expect(homePage.Titulo)
            .ToBeVisibleAsync();

        await Expect(homePage.LinkEntrar)
            .ToBeVisibleAsync();

        await Expect(homePage.BotaoEntrarNoSistema)
            .ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task Deve_ExibirTelaDeLogin_AoClicarEmEntrar()
    {
        // Arrange
        HomePage homePage = new(Page, UrlBase);

        await homePage.IrParaAsync();

        // Act
        await homePage.LinkEntrar.ClickAsync();

        // Assert
        EntrarPage entrarPage = new(Page, UrlBase);

        await Expect(entrarPage.Titulo)
            .ToBeVisibleAsync();

        await Expect(entrarPage.Email)
            .ToBeVisibleAsync();

        await Expect(entrarPage.Senha)
            .ToBeVisibleAsync();

        await Expect(entrarPage.BotaoEntrar)
            .ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task Deve_RegistrarEAutenticar_Usuario()
    {
        // Arrange
        string email =
            $"novo.usuario.{Guid.NewGuid():N}@teste.local";

        const string senha = "Senha123!";

        RegistrarPage registrarPage =
            new(Page, UrlBase);

        await registrarPage.IrParaAsync();

        // Act
        await registrarPage.PreencherAsync(
            "Usuário de Teste",
            email,
            senha
        );

        await registrarPage.ConfirmarAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync($"{UrlBase}/");

        HomePage homePage =
            new(Page, UrlBase);

        await Expect(homePage.Titulo)
            .ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task Deve_EntrarEAutenticar_Usuario_Valido()
    {
        // Arrange
        string email =
            $"login.valido.{Guid.NewGuid():N}@teste.local";

        const string senha = "Senha123!";

        await RegistrarUsuarioAsync(
            email,
            senha
        );

        EntrarPage entrarPage =
            new(Page, UrlBase);

        // Act
        await entrarPage.IrParaAsync();

        await entrarPage.PreencherAsync(
            email,
            senha
        );

        await entrarPage.ConfirmarAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync($"{UrlBase}/");

        HomePage homePage =
            new(Page, UrlBase);

        await Expect(homePage.Titulo)
            .ToBeVisibleAsync();
    }
}