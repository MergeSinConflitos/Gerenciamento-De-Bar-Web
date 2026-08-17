using Microsoft.Playwright;

namespace GerenciamentoDeBar.Testes.E2E.Modulos.ModuloGarcom;

public class GarcomExcluirPage
{
    private readonly IPage page;
    private readonly string urlBase;

    public GarcomExcluirPage(IPage page, string urlBase)
    {
        this.page = page;
        this.urlBase = urlBase;
    }

    // =========================================================
    // URL
    // =========================================================

    public string Url(Guid id) =>
        $"{urlBase}/Garcom/Excluir?id={id}";


    // =========================================================
    // TÍTULO
    // =========================================================

    public ILocator Titulo =>
        page.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Excluir Garçom",
                Exact = true
            }
        );


    // =========================================================
    // DADOS DO GARÇOM
    // =========================================================

    public ILocator Nome =>
        page.Locator("h2").Filter(
            new()
            {
                HasText = ""
            }
        );

    public ILocator Telefone =>
        page.Locator("strong").Nth(0);

    public ILocator Cpf =>
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
            "Tem certeza que deseja excluir este garçom?"
        );


    // =========================================================
    // BOTÕES
    // =========================================================

    public ILocator ConfirmarExclusao =>
        page.GetByRole(
            AriaRole.Button,
            new()
            {
                Name = "Sim, Excluir Garçom"
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