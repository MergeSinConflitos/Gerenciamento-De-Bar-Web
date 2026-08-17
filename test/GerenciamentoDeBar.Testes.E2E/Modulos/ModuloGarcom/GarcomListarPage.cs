using Microsoft.Playwright;

namespace GerenciamentoDeBar.Testes.E2E.Modulos.ModuloGarcom;

public class GarcomListarPage
{
    private readonly IPage page;
    private readonly string urlBase;

    public GarcomListarPage(IPage page, string urlBase)
    {
        this.page = page;
        this.urlBase = urlBase;
    }

    public string Url =>
        $"{urlBase}/Garcom/Listar";


    // =========================================================
    // CABEÇALHO
    // =========================================================

    public ILocator Titulo =>
        page.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Listagem de Garçons",
                Exact = true
            }
        );

    public ILocator CadastrarNovo =>
        page.Locator("#btn-cadastrar-garcom");


    // =========================================================
    // MENSAGENS
    // =========================================================

    public ILocator MensagemErro =>
        page.Locator("#mensagem-erro-garcom");

    public ILocator EstadoVazio =>
        page.Locator("#lista-garcons-vazia");


    // =========================================================
    // FILTRO
    // =========================================================

    public ILocator CampoNome =>
        page.Locator("#nome");

    public ILocator BotaoPesquisar =>
        page.Locator("#btn-pesquisar-garcom");

    public ILocator BotaoLimpar =>
        page.Locator("#btn-limpar-filtro-garcom");


    // =========================================================
    // LISTAGEM
    // =========================================================

    public ILocator ListaGarcons =>
        page.Locator("#lista-garcons");

    public ILocator Cards =>
        page.Locator(".garcom-card");


    // =========================================================
    // CARD ESPECÍFICO
    // =========================================================

    public ILocator CardGarcom(string nome)
    {
        return page
            .Locator(".garcom-card")
            .Filter(new()
            {
                Has = page
                    .Locator(".garcom-nome")
                    .GetByText(
                        nome,
                        new()
                        {
                            Exact = true
                        }
                    )
            });
    }


    // =========================================================
    // DADOS DO GARÇOM
    // =========================================================

    public ILocator Nome(string nome)
    {
        return CardGarcom(nome)
            .Locator(".garcom-nome");
    }

    public ILocator Telefone(string nome)
    {
        return CardGarcom(nome)
            .Locator(".garcom-telefone");
    }

    public ILocator Cpf(string nome)
    {
        return CardGarcom(nome)
            .Locator(".garcom-cpf");
    }

    public ILocator Id(string nome)
    {
        return CardGarcom(nome)
            .Locator(".garcom-id");
    }


    // =========================================================
    // AÇÕES
    // =========================================================

    public ILocator Editar(string nome)
    {
        return CardGarcom(nome)
            .Locator(".btn-editar-garcom");
    }

    public ILocator Excluir(string nome)
    {
        return CardGarcom(nome)
            .Locator(".btn-excluir-garcom");
    }
}