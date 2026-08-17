using GerenciamentoDeBar.Testes.E2E.Compartilhado;

namespace GerenciamentoDeBar.Testes.E2E.Modulos.ModuloGarcom;

[TestClass]
public sealed class GarcomE2ETests : E2ETestsBase
{
    private async Task AutenticarUsuarioAsync()
    {
        string email =
            $"garcom.teste.{Guid.NewGuid():N}@teste.local";

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
    public async Task Deve_ExibirPaginaDeListagem_DeGarcons()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        GarcomListarPage listarPage =
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
    public async Task Deve_ExibirTelaDeCadastro_DeGarcom()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        GarcomListarPage listarPage =
            new(Page, UrlBase);

        await Page.GotoAsync(listarPage.Url);

        // Act
        await listarPage.CadastrarNovo.ClickAsync();

        // Assert
        GarcomFormPage formPage =
            new(Page, UrlBase);

        await Expect(formPage.TituloCadastrar)
            .ToBeVisibleAsync();

        await Expect(formPage.Nome)
            .ToBeVisibleAsync();

        await Expect(formPage.Telefone)
            .ToBeVisibleAsync();

        await Expect(formPage.Cpf)
            .ToBeVisibleAsync();

        await Expect(formPage.Salvar)
            .ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task Deve_CadastrarGarcom_ComDadosValidos()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        string nome =
            $"João {Guid.NewGuid():N}".Substring(0, 12);

        const string telefone = "(49) 99999-9999";
        const string cpf = "529.982.247-25";

        GarcomFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        // Act
        await formPage.Nome.FillAsync(nome);
        await formPage.Telefone.FillAsync(telefone);
        await formPage.Cpf.FillAsync(cpf);

        await formPage.Salvar.ClickAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync(
                $"{UrlBase}/Garcom/Listar"
            );

        GarcomListarPage listarPage =
            new(Page, UrlBase);

        await Expect(
            listarPage.Nome(nome)
        ).ToBeVisibleAsync();

        await Expect(
            listarPage.Telefone(nome)
        ).ToHaveTextAsync(telefone);

        await Expect(
            listarPage.Cpf(nome)
        ).ToHaveTextAsync(cpf);
    }


    // =========================================================
    // VALIDAÇÃO - NOME
    // =========================================================

    [TestMethod]
    public async Task NaoDeve_CadastrarGarcom_ComNomeVazio()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        GarcomFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        await formPage.Nome.FillAsync("");
        await formPage.Telefone.FillAsync("(49) 99999-9999");
        await formPage.Cpf.FillAsync("529.982.247-25");

        // Act
        await formPage.Salvar.ClickAsync();

        // Assert
        await Expect(formPage.ErroNome)
            .ToBeVisibleAsync();

        await Expect(Page)
            .ToHaveURLAsync(formPage.UrlCadastrar);
    }


    [TestMethod]
    public async Task NaoDeve_CadastrarGarcom_ComNomeMuitoCurto()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        GarcomFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        // Act
        await formPage.Nome.FillAsync("A");
        await formPage.Telefone.FillAsync("(49) 99999-9999");
        await formPage.Cpf.FillAsync("529.982.247-25");

        await formPage.Salvar.ClickAsync();

        // Assert
        await Expect(formPage.ErroNome)
            .ToBeVisibleAsync();
    }


    // =========================================================
    // VALIDAÇÃO - TELEFONE
    // =========================================================

    [TestMethod]
    public async Task NaoDeve_CadastrarGarcom_ComTelefoneVazio()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        GarcomFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        // Act
        await formPage.Nome.FillAsync("João");
        await formPage.Telefone.FillAsync("");
        await formPage.Cpf.FillAsync("529.982.247-25");

        await formPage.Salvar.ClickAsync();

        // Assert
        await Expect(formPage.ErroTelefone)
            .ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task NaoDeve_CadastrarGarcom_ComTelefoneInvalido()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        GarcomFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        // Act
        await formPage.Nome.FillAsync("João");
        await formPage.Telefone.FillAsync("123456");
        await formPage.Cpf.FillAsync("529.982.247-25");

        await formPage.Salvar.ClickAsync();

        // Assert
        await Expect(formPage.ErroTelefone)
            .ToBeVisibleAsync();
    }


    // =========================================================
    // VALIDAÇÃO - CPF
    // =========================================================

    [TestMethod]
    public async Task NaoDeve_CadastrarGarcom_ComCpfVazio()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        GarcomFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        // Act
        await formPage.Nome.FillAsync("João");
        await formPage.Telefone.FillAsync("(49) 99999-9999");
        await formPage.Cpf.FillAsync("");

        await formPage.Salvar.ClickAsync();

        // Assert
        await Expect(formPage.ErroCpf)
            .ToBeVisibleAsync();
    }


    [TestMethod]
    public async Task NaoDeve_CadastrarGarcom_ComCpfInvalido()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        GarcomFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        await formPage.DesabilitarValidacaoNativaAsync();

        // Act
        await formPage.Nome.FillAsync("João");
        await formPage.Telefone.FillAsync("(49) 99999-9999");
        await formPage.Cpf.FillAsync("012.859.985-01");

        await formPage.Salvar.ClickAsync();

        // Assert
        await Expect(formPage.ResumoErros)
       .ToContainTextAsync("O CPF informado é inválido");

        await Expect(Page)
       .ToHaveURLAsync(formPage.UrlCadastrar);

    }


    // =========================================================
    // CPF DUPLICADO
    // =========================================================

    [TestMethod]
    public async Task NaoDeve_CadastrarGarcom_ComCpfDuplicado()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        const string cpf = "529.982.247-25";

        GarcomFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        await formPage.Nome.FillAsync("João");
        await formPage.Telefone.FillAsync("(49) 99999-9999");
        await formPage.Cpf.FillAsync(cpf);

        await formPage.Salvar.ClickAsync();

        // Act
        await formPage.IrParaCadastrar();

        await formPage.Nome.FillAsync("Maria");
        await formPage.Telefone.FillAsync("(49) 98888-8888");
        await formPage.Cpf.FillAsync(cpf);

        await formPage.Salvar.ClickAsync();

        // Assert
        await Expect(formPage.ErroCpfDuplicado)
            .ToBeVisibleAsync();


        await Expect(Page)
            .ToHaveURLAsync(formPage.UrlCadastrar);
    }


    // =========================================================
    // TELEFONE DUPLICADO
    // =========================================================

    [TestMethod]
    public async Task NaoDeve_CadastrarGarcom_ComTelefoneDuplicado()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        const string telefone = "(49) 99999-9999";

        GarcomFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        await formPage.Nome.FillAsync("João");
        await formPage.Telefone.FillAsync(telefone);
        await formPage.Cpf.FillAsync("529.982.247-25");

        await formPage.Salvar.ClickAsync();

        // Act
        await formPage.IrParaCadastrar();

        await formPage.Nome.FillAsync("Maria");
        await formPage.Telefone.FillAsync(telefone);
        await formPage.Cpf.FillAsync("111.444.777-35");

        await formPage.Salvar.ClickAsync();

        // Assert
        await Expect(formPage.ErroTelefoneDuplicado)
            .ToBeVisibleAsync();

        await Expect(Page)
            .ToHaveURLAsync(formPage.UrlCadastrar);
    }


    // =========================================================
    // EDIÇÃO
    // =========================================================

    [TestMethod]
    public async Task Deve_EditarGarcom_ComDadosValidos()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        string nomeInicial =
            $"João {Guid.NewGuid():N}".Substring(0, 12);

        string nomeAtualizado =
            $"Carlos {Guid.NewGuid():N}".Substring(0, 14);

        const string telefoneInicial = "(49) 99999-9999";
        const string cpfInicial = "529.982.247-25";

        const string telefoneAtualizado = "(49) 98888-8888";
        const string cpfAtualizado = "111.444.777-35";

        GarcomFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        await formPage.Nome.FillAsync(nomeInicial);
        await formPage.Telefone.FillAsync(telefoneInicial);
        await formPage.Cpf.FillAsync(cpfInicial);

        await formPage.Salvar.ClickAsync();

        GarcomListarPage listarPage =
            new(Page, UrlBase);

        await Page.GotoAsync(listarPage.Url);

        string idTexto =
            await listarPage.Id(nomeInicial).InnerTextAsync();

        Guid id =
            Guid.Parse(idTexto);

        // Act
        await formPage.IrParaEditar(id);

        await formPage.Nome.FillAsync(nomeAtualizado);
        await formPage.Telefone.FillAsync(telefoneAtualizado);
        await formPage.Cpf.FillAsync(cpfAtualizado);

        await formPage.Salvar.ClickAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync(
                $"{UrlBase}/Garcom/Listar"
            );

        await Expect(
            listarPage.Nome(nomeAtualizado)
        ).ToBeVisibleAsync();

        await Expect(
            listarPage.Telefone(nomeAtualizado)
        ).ToHaveTextAsync(telefoneAtualizado);

        await Expect(
            listarPage.Cpf(nomeAtualizado)
        ).ToHaveTextAsync(cpfAtualizado);
    }


    // =========================================================
    // PESQUISA POR NOME
    // =========================================================

    [TestMethod]
    public async Task Deve_PesquisarGarcom_PorNome()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        string nome =
            $"João {Guid.NewGuid():N}".Substring(0, 12);

        GarcomFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        await formPage.Nome.FillAsync(nome);
        await formPage.Telefone.FillAsync("(49) 99999-9999");
        await formPage.Cpf.FillAsync("529.982.247-25");

        await formPage.Salvar.ClickAsync();

        GarcomListarPage listarPage =
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
    public async Task Deve_ExibirTelaDeExclusao_DeGarcom()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        string nome =
            $"João {Guid.NewGuid():N}".Substring(0, 12);

        GarcomFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        await formPage.Nome.FillAsync(nome);
        await formPage.Telefone.FillAsync("(49) 99999-9999");
        await formPage.Cpf.FillAsync("529.982.247-25");

        await formPage.Salvar.ClickAsync();

        GarcomListarPage listarPage =
            new(Page, UrlBase);

        await Page.GotoAsync(listarPage.Url);

        // Act
        await listarPage.Excluir(nome).ClickAsync();

        // Assert
        GarcomExcluirPage excluirPage =
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
    public async Task Deve_ExcluirGarcom()
    {
        // Arrange
        await AutenticarUsuarioAsync();

        string nome =
            $"João {Guid.NewGuid():N}".Substring(0, 12);

        GarcomFormPage formPage =
            new(Page, UrlBase);

        await formPage.IrParaCadastrar();

        await formPage.Nome.FillAsync(nome);
        await formPage.Telefone.FillAsync("(49) 99999-9999");
        await formPage.Cpf.FillAsync("529.982.247-25");

        await formPage.Salvar.ClickAsync();

        GarcomListarPage listarPage =
            new(Page, UrlBase);

        await Page.GotoAsync(listarPage.Url);

        // Act
        await listarPage.Excluir(nome).ClickAsync();

        GarcomExcluirPage excluirPage =
            new(Page, UrlBase);

        await excluirPage.ConfirmarExclusao.ClickAsync();

        // Assert
        await Expect(Page)
            .ToHaveURLAsync(
                $"{UrlBase}/Garcom/Listar"
            );

        await Expect(
            listarPage.Nome(nome)
        ).ToHaveCountAsync(0);
    }
}