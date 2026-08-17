using Microsoft.Playwright;

namespace GerenciamentoDeBar.Testes.E2E.Modulos.ModuloProduto;

public class ProdutoExcluirPage
{
    private readonly IPage page;
    private readonly string urlBase;

    public ProdutoExcluirPage(IPage page, string urlBase)
    {
        this.page = page;
        this.urlBase = urlBase;
    }

    // =========================================================
    // URL
    // =========================================================

    public string Url(Guid id) =>
        $"{urlBase}/Produto/Excluir?id={id}";


    // =========================================================
    // TÍTULO
    // =========================================================

    public ILocator Titulo =>
        page.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Excluir Produto",
                Exact = true
            }
        );


    // =========================================================
    // DADOS DO PRODUTO
    // =========================================================

    public ILocator Nome =>
        page.Locator("h2");

    public ILocator QuantidadeEstoque =>
        page.Locator("strong").Nth(0);

    public ILocator Preco =>
        page.Locator("strong").Nth(1);

    public ILocator Id =>
        page.Locator(".text-muted.small.text-break");


    // =========================================================
    // MENSAGEM DE CONFIRMAÇÃO
    // =========================================================

    public ILocator Aviso =>
        page.GetByText(
            "Esta ação não poderá ser desfeita."
        );

    public ILocator PerguntaConfirmacao =>
        page.GetByText(
            "Tem certeza que deseja excluir este produto?"
        );


    // =========================================================
    // BOTÕES
    // =========================================================

    public ILocator ConfirmarExclusao =>
        page.GetByRole(
            AriaRole.Button,
            new()
            {
                Name = "Sim, Excluir Produto"
            }
        );

    public ILocator Cancelar =>
        page.GetByRole(
            AriaRole.Link,
            new()
            {
                Name = "Cancelar"
            }
        );

    public ILocator Voltar =>
        page.GetByRole(
            AriaRole.Link,
            new()
            {
                Name = "Voltar"
            }
        );


    // =========================================================
    // MENSAGEM DE ERRO
    // =========================================================

    public ILocator MensagemErro =>
        page.Locator(".alert-danger");


    // =========================================================
    // NAVEGAÇÃO
    // =========================================================

    public async Task IrPara(Guid id)
    {
        await page.GotoAsync(Url(id));
    }
}