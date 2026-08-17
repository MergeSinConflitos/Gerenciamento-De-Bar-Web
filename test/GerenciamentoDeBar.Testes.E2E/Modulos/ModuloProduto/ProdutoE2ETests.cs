using GerenciamentoDeBar.Testes.E2E.Compartilhado;

namespace GerenciamentoDeBar.Testes.E2E.Modulos.ModuloProduto;

[TestClass]
public sealed class ProdutoE2ETests : E2ETestsBase
{
    private async Task AutenticarUsuarioAsync()
    {
        string email =
            $"produto.teste.{Guid.NewGuid():N}@teste.local";

        const string senha = "Senha123!";

        await RegistrarEEntrarAsync(
            email,
            senha
        );
    }


    // =========================================================
    // LISTAGEM
    // =========================================================

    [TestMethod]
    public async Task Deve_ExibirPaginaDeListagem_DeProdutos()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        ProdutoListarPage listarPage =
            new(Page, UrlBase);

        // Act
        await Page.GotoAsync(listarPage.Url);

        // Assert
        await Expect(listarPage.Titulo)
            .ToBeVisibleAsync();

        await Expect(listarPage.CadastrarNovo)
            .ToBeVisibleAsync();

        await Expect(listarPage.CampoNome)
            .ToBeVisibleAsync();

        await Expect(listarPage.BotaoPesquisar)
            .ToBeVisibleAsync();
    }


    // =========================================================
    // CADASTRO
    // =========================================================

    [TestMethod]
    public async Task Deve_ExibirTelaDeCadastro_DeProduto()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        ProdutoListarPage listarPage =
            new(Page, UrlBase);

        await Page.GotoAsync(listarPage.Url);

        // Act
        await listarPage.CadastrarNovo.ClickAsync();

        // Assert
        ProdutoFormPage formPage =
            new(Page, UrlBase);

        await Expect(formPage.TituloCadastrar)
            .ToBeVisibleAsync();

        await Expect(formPage.Nome)
            .ToBeVisibleAsync();

        await Expect(formPage.Preco)
            .ToBeVisibleAsync();

        await Expect(formPage.Salvar)
            .ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task Deve_CadastrarProduto_ComDadosValidos()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        string nome =
            $"Cerveja {Guid.NewGuid():N}".Substring(0, 14);

        const decimal preco = 8.50m;

        ProdutoFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        // Act
        await formPage.Nome.FillAsync(nome);

        await formPage.Preco.FillAsync("8.50");
        await formPage.Salvar.ClickAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync(
                $"{UrlBase}/Produto/Listar"
            );

        ProdutoListarPage listarPage =
            new(Page, UrlBase);

        await Expect(
            listarPage.Nome(nome)
        ).ToBeVisibleAsync();
        await Expect(
            listarPage.Preco(nome)
        ).ToHaveTextAsync(
            $"R$ {preco.ToString("F2")}"
        );
    }


    // =========================================================
    // VALIDAÇÃO - NOME
    // =========================================================

    [TestMethod]
    public async Task NaoDeve_CadastrarProduto_ComNomeVazio()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        ProdutoFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        // Act
        await formPage.Nome.FillAsync("");

        await formPage.Preco.FillAsync("8.50");

        await formPage.Salvar.ClickAsync();

        // Assert
        await Expect(formPage.ErroNome)
            .ToBeVisibleAsync();

        await Expect(Page)
            .ToHaveURLAsync(
                formPage.UrlCadastrar
            );
    }


    [TestMethod]
    public async Task NaoDeve_CadastrarProduto_ComNomeMuitoCurto()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        ProdutoFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        // Act
        await formPage.Nome.FillAsync("A");

        await formPage.Preco.FillAsync("8.50");

        await formPage.Salvar.ClickAsync();

        // Assert
        await Expect(formPage.ErroNome)
            .ToBeVisibleAsync();
    }


    // =========================================================
    // VALIDAÇÃO - PREÇO
    // =========================================================

    [TestMethod]
    public async Task NaoDeve_CadastrarProduto_ComPrecoVazio()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        ProdutoFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        // Act
        await formPage.Nome.FillAsync("Cerveja");

        await formPage.Preco.FillAsync("");

        await formPage.Salvar.ClickAsync();

        // Assert
        await Expect(formPage.ErroPreco)
            .ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task NaoDeve_CadastrarProduto_ComPrecoNegativo()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        ProdutoFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        await formPage.DesabilitarValidacaoNativaAsync();

        // Act
        await formPage.Nome.FillAsync("Cerveja");

        await formPage.Preco.FillAsync("-5");

        await formPage.Salvar.ClickAsync();

        // Assert
        await Expect(formPage.ErroPreco)
            .ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task NaoDeve_CadastrarProduto_ComNomeDuplicado()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        const string nome = "Cerveja";

        ProdutoFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        await formPage.Nome.FillAsync(nome);
        await formPage.Preco.FillAsync("8.50");

        await formPage.Salvar.ClickAsync();

        // Act
        await formPage.IrParaCadastrar();

        await formPage.Nome.FillAsync(nome);
        await formPage.Preco.FillAsync("10.50");

        await formPage.Salvar.ClickAsync();

        // Assert
        await Expect(formPage.ErroNomeDuplicado)
            .ToBeVisibleAsync();

        await Expect(Page)
            .ToHaveURLAsync(formPage.UrlCadastrar);
    }

    // =========================================================
    // EDIÇÃO
    // =========================================================
    [TestMethod]
    public async Task Deve_EditarProduto_ComDadosValidos()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        string nomeInicial =
            $"Cerveja {Guid.NewGuid():N}".Substring(0, 14);

        string nomeAtualizado =
            $"Refrigerante {Guid.NewGuid():N}".Substring(0, 18);

        const decimal precoInicial = 8.50m;
        const decimal precoAtualizado = 6.50m;

        ProdutoFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        await formPage.Nome.FillAsync(nomeInicial);

        await formPage.PreencherPrecoAsync(precoInicial);

        await formPage.Salvar.ClickAsync();

        ProdutoListarPage listarPage =
            new(Page, UrlBase);

        await Page.GotoAsync(listarPage.Url);

        string idTexto =
            await listarPage.Id(nomeInicial)
                .GetAttributeAsync("data-id");

        Guid id = Guid.Parse(idTexto!);

        // Act
        await formPage.IrParaEditar(id);

        await formPage.Nome.FillAsync(nomeAtualizado);

        await formPage.PreencherPrecoAsync(precoAtualizado);

        await formPage.Salvar.ClickAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync(
                $"{UrlBase}/Produto/Listar"
            );

        await Expect(
            listarPage.Nome(nomeAtualizado)
        ).ToBeVisibleAsync();

        await Expect(
            listarPage.Preco(nomeAtualizado)
        ).ToHaveTextAsync(
            "R$ 6,50"
        );
    }

    // =========================================================
    // PESQUISA POR NOME
    // =========================================================

    [TestMethod]
    public async Task Deve_PesquisarProduto_PorNome()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        string nome =
            $"Cerveja {Guid.NewGuid():N}".Substring(0, 14);

        ProdutoFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        await formPage.Nome.FillAsync(nome);

        await formPage.Preco.FillAsync("8.50");

        await formPage.Salvar.ClickAsync();

        ProdutoListarPage listarPage =
            new(Page, UrlBase);

        await Page.GotoAsync(listarPage.Url);

        // Act
        await listarPage.CampoNome.FillAsync(nome);

        await listarPage.BotaoPesquisar.ClickAsync();

        // Assert
        await Expect(
            listarPage.Nome(nome)
        ).ToBeVisibleAsync();
    }


    // =========================================================
    // EXCLUSÃO
    // =========================================================

    [TestMethod]
    public async Task Deve_ExibirTelaDeExclusao_DeProduto()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        string nome =
            $"Cerveja {Guid.NewGuid():N}".Substring(0, 14);

        ProdutoFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        await formPage.Nome.FillAsync(nome);

        await formPage.Preco.FillAsync("8.50");

        await formPage.Salvar.ClickAsync();

        ProdutoListarPage listarPage =
            new(Page, UrlBase);

        await Page.GotoAsync(listarPage.Url);

        // Act
        await listarPage.Excluir(nome).ClickAsync();

        // Assert
        ProdutoExcluirPage excluirPage =
            new(Page, UrlBase);

        await Expect(excluirPage.Titulo)
            .ToBeVisibleAsync();

        await Expect(excluirPage.Aviso)
            .ToBeVisibleAsync();

        await Expect(excluirPage.PerguntaConfirmacao)
            .ToBeVisibleAsync();

        await Expect(excluirPage.ConfirmarExclusao)
            .ToBeVisibleAsync();

        await Expect(excluirPage.Cancelar)
            .ToBeVisibleAsync();

        await Expect(excluirPage.Voltar)
            .ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task Deve_ExcluirProduto()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        string nome =
            $"Cerveja {Guid.NewGuid():N}".Substring(0, 14);

        ProdutoFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        await formPage.Nome.FillAsync(nome);

        await formPage.Preco.FillAsync("8.50");

        await formPage.Salvar.ClickAsync();

        ProdutoListarPage listarPage =
            new(Page, UrlBase);

        await Page.GotoAsync(listarPage.Url);

        // Act
        await listarPage.Excluir(nome).ClickAsync();

        ProdutoExcluirPage excluirPage =
            new(Page, UrlBase);

        await excluirPage.ConfirmarExclusao.ClickAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync(
                $"{UrlBase}/Produto/Listar"
            );

        await Expect(
            listarPage.Nome(nome)
        ).ToHaveCountAsync(0);
    }
}