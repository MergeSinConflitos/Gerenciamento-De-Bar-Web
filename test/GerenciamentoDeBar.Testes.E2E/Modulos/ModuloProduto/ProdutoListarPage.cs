using Microsoft.Playwright;

namespace GerenciamentoDeBar.Testes.E2E.Modulos.ModuloProduto;

public class ProdutoListarPage
{
    private readonly IPage page;
    private readonly string urlBase;

    public ProdutoListarPage(IPage page, string urlBase)
    {
        this.page = page;
        this.urlBase = urlBase;
    }

    public string Url =>
        $"{urlBase}/Produto/Listar";


    // =========================================================
    // CABEÇALHO
    // =========================================================

    public ILocator Titulo =>
        page.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Listagem de Produtos",
                Exact = true
            }
        );

    public ILocator CadastrarNovo =>
        page.Locator("#btn-cadastrar-produto");


    // =========================================================
    // MENSAGENS
    // =========================================================

    public ILocator MensagemErro =>
        page.Locator("#mensagem-erro-produto");

    public ILocator EstadoVazio =>
        page.Locator("#lista-produtos-vazia");


    // =========================================================
    // FILTRO
    // =========================================================

    public ILocator CampoNome =>
        page.Locator("#nome");

    public ILocator BotaoPesquisar =>
        page.Locator("#btn-pesquisar-produto");

    public ILocator BotaoLimpar =>
        page.Locator("#btn-limpar-filtro-produto");


    // =========================================================
    // LISTAGEM
    // =========================================================

    public ILocator ListaProdutos =>
        page.Locator("#lista-produtos");

    public ILocator Cards =>
        page.Locator(".produto-card");


    // =========================================================
    // CARD ESPECÍFICO
    // =========================================================

    public ILocator CardProduto(string nome)
    {
        return page
            .Locator(".produto-card")
            .Filter(new()
            {
                Has = page
                    .Locator(".produto-nome")
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
    // DADOS DO PRODUTO
    // =========================================================

    public ILocator Nome(string nome)
    {
        return CardProduto(nome)
            .Locator(".produto-nome");
    }

    public ILocator Preco(string nome)
    {
        return CardProduto(nome)
            .Locator(".produto-preco");
    }

    public ILocator Id(string nome)
    {
        return CardProduto(nome)
            .Locator(".produto-id");
    }


    // =========================================================
    // AÇÕES
    // =========================================================

    public ILocator Editar(string nome)
    {
        return CardProduto(nome)
            .Locator(".btn-editar-produto");
    }

    public ILocator Excluir(string nome)
    {
        return CardProduto(nome)
            .Locator(".btn-excluir-produto");
    }


}
