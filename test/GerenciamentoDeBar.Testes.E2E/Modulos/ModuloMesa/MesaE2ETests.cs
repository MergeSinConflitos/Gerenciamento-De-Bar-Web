using GerenciamentoDeBar.Testes.E2E.Compartilhado;

namespace GerenciamentoDeBar.Testes.E2E.Modulos.ModuloMesa;

[TestClass]
public sealed class MesaE2ETests : E2ETestsBase
{
    private async Task AutenticarUsuarioAsync()
    {
        string email =
            $"mesa.teste.{Guid.NewGuid():N}@teste.local";

        const string senha = "Senha123!";

        await RegistrarEEntrarAsync(
            email,
            senha
        );
    }


    [TestMethod]
    public async Task Deve_ExibirPaginaDeListagem_DeMesas()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        MesaListarPage listarPage =
            new(Page, UrlBase);

        // Act
        await listarPage.IrParaAsync();

        // Assert
        await Expect(listarPage.Titulo)
            .ToBeVisibleAsync();

        await Expect(listarPage.CadastrarNovo)
            .ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task Deve_ExibirTelaDeCadastro_DeMesa()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        MesaListarPage listarPage =
            new(Page, UrlBase);

        await listarPage.IrParaAsync();

        // Act
        await listarPage.CadastrarAsync();

        // Assert
        MesaFormPage formPage =
            new(Page, UrlBase);

        await Expect(formPage.NumeroDaMesa)
            .ToBeVisibleAsync();

        await Expect(formPage.QuantidadeDeLugares)
            .ToBeVisibleAsync();

        await Expect(formPage.Confirmar)
            .ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task Deve_CadastrarMesa_ComDadosValidos()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        int numeroDaMesa =
            Random.Shared.Next(1000, 9999);

        MesaFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastroAsync();

        // Act
        await formPage.PreencherAsync(
            numeroDaMesa,
            4
        );

        await formPage.ConfirmarAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync(
                $"{UrlBase}/Mesa/Listar"
            );

        MesaListarPage listarPage =
            new(Page, UrlBase);

        await Expect(
            listarPage.NumeroDaMesa(numeroDaMesa)
        ).ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task NaoDeve_CadastrarMesa_ComNumeroZero()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        MesaFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastroAsync();

        await formPage.DesabilitarValidacaoNativaAsync();

        // Act
        await formPage.PreencherAsync(
            0,
            4
        );

        await formPage.ConfirmarAsync();

        // Assert
        await Expect(formPage.ErroNumeroDaMesa)
            .ToBeVisibleAsync();

        await Expect(Page)
            .ToHaveURLAsync(formPage.Url);
    }


    [TestMethod]
    public async Task NaoDeve_CadastrarMesa_ComNumeroNegativo()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        MesaFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastroAsync();

        await formPage.DesabilitarValidacaoNativaAsync();

        // Act
        await formPage.PreencherAsync(
            -1,
            4
        );

        await formPage.ConfirmarAsync();

        // Assert
        await Expect(formPage.ErroNumeroDaMesa)
            .ToBeVisibleAsync();

        await Expect(Page)
            .ToHaveURLAsync(formPage.Url);
    }


    [TestMethod]
    public async Task NaoDeve_CadastrarMesa_ComQuantidadeDeLugaresMaiorQueDez()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        MesaFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastroAsync();

        await formPage.DesabilitarValidacaoNativaAsync();

        // Act
        await formPage.PreencherAsync(
            Random.Shared.Next(1000, 9999),
            11
        );

        await formPage.ConfirmarAsync();

        // Assert
        await Expect(formPage.ErroQuantidadeDeLugares)
            .ToBeVisibleAsync();

        await Expect(Page)
            .ToHaveURLAsync(formPage.Url);
    }


    [TestMethod]
    public async Task NaoDeve_CadastrarMesa_ComQuantidadeDeLugaresZero()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        MesaFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastroAsync();

        await formPage.DesabilitarValidacaoNativaAsync();

        // Act
        await formPage.PreencherAsync(
            Random.Shared.Next(1000, 9999),
            0
        );

        await formPage.ConfirmarAsync();

        // Assert
        await Expect(formPage.ErroQuantidadeDeLugares)
            .ToBeVisibleAsync();

        await Expect(Page)
            .ToHaveURLAsync(formPage.Url);
    }


    [TestMethod]
    public async Task NaoDeve_CadastrarMesa_ComNumeroDuplicado()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        int numeroDaMesa =
            Random.Shared.Next(1000, 9999);

        MesaFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastroAsync();

        await formPage.PreencherAsync(
            numeroDaMesa,
            4
        );

        await formPage.ConfirmarAsync();

        // Act
        await formPage.IrParaCadastroAsync();

        await formPage.PreencherAsync(
            numeroDaMesa,
            6
        );

        await formPage.ConfirmarAsync();

        // Assert
        await Expect(formPage.ErroNumeroDaMesaDuplicado)
            .ToBeVisibleAsync();

        await Expect(Page)
            .ToHaveURLAsync(formPage.Url);
    }


    [TestMethod]
    public async Task Deve_EditarMesa_ComDadosValidos()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        int numeroInicial =
            Random.Shared.Next(1000, 4999);

        int numeroAtualizado =
            Random.Shared.Next(5000, 9999);

        MesaFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastroAsync();

        await formPage.PreencherAsync(
            numeroInicial,
            4
        );

        await formPage.ConfirmarAsync();

        MesaListarPage listarPage =
            new(Page, UrlBase);

        await listarPage.IrParaAsync();

        // Act
        await listarPage.EditarAsync(
            numeroInicial
        );

        formPage =
            new(Page, UrlBase);

        await formPage.PreencherAsync(
            numeroAtualizado,
            6
        );

        await formPage.ConfirmarAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync(
                $"{UrlBase}/Mesa/Listar"
            );

        await Expect(
            listarPage.NumeroDaMesa(numeroAtualizado)
        ).ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task Deve_ExibirTelaDeExclusao_DeMesa()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        int numeroDaMesa =
            Random.Shared.Next(1000, 9999);

        MesaFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastroAsync();

        await formPage.PreencherAsync(
            numeroDaMesa,
            4
        );

        await formPage.ConfirmarAsync();

        MesaListarPage listarPage =
            new(Page, UrlBase);

        await listarPage.IrParaAsync();

        // Act
        await listarPage.ExcluirAsync(
            numeroDaMesa
        );

        // Assert
        MesaExcluirPage excluirPage =
            new(Page, UrlBase);

        await Expect(excluirPage.MensagemConfirmacao)
            .ToBeVisibleAsync();

        await Expect(excluirPage.Confirmar)
            .ToBeVisibleAsync();

        await Expect(excluirPage.Voltar)
            .ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task Deve_ExcluirMesa()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        int numeroDaMesa =
            Random.Shared.Next(1000, 9999);

        MesaFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastroAsync();

        await formPage.PreencherAsync(
            numeroDaMesa,
            4
        );

        await formPage.ConfirmarAsync();

        MesaListarPage listarPage =
            new(Page, UrlBase);

        await listarPage.IrParaAsync();

        // Act
        await listarPage.ExcluirAsync(
            numeroDaMesa
        );

        MesaExcluirPage excluirPage =
            new(Page, UrlBase);

        await excluirPage.ConfirmarAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync(
                $"{UrlBase}/Mesa/Listar"
            );

        await Expect(
            listarPage.NumeroDaMesa(numeroDaMesa)
        ).ToHaveCountAsync(0);
    }


    [TestMethod]
    public async Task Deve_PesquisarMesa_PorNumero()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        int numeroDaMesa =
            Random.Shared.Next(1000, 9999);

        MesaFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastroAsync();

        await formPage.PreencherAsync(
            numeroDaMesa,
            4
        );

        await formPage.ConfirmarAsync();

        MesaListarPage listarPage =
            new(Page, UrlBase);

        await listarPage.IrParaAsync();

        // Act
        await listarPage.PesquisarPorNumeroAsync(
            numeroDaMesa
        );

        // Assert
        await Expect(
            listarPage.NumeroDaMesa(numeroDaMesa)
        ).ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task Deve_PesquisarMesa_PorStatusLivre()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        int numeroDaMesa =
            Random.Shared.Next(1000, 9999);

        MesaFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastroAsync();

        await formPage.PreencherAsync(
            numeroDaMesa,
            4
        );

        await formPage.ConfirmarAsync();

        MesaListarPage listarPage =
            new(Page, UrlBase);

        await listarPage.IrParaAsync();

        // Act
        await listarPage.PesquisarPorStatusAsync(
            "Livre"
        );

        // Assert
        await Expect(
            listarPage.NumeroDaMesa(numeroDaMesa)
        ).ToBeVisibleAsync();

        await Expect(
            listarPage.StatusDaMesa(
                numeroDaMesa,
                "Livre"
            )
        ).ToBeVisibleAsync();
    }
}