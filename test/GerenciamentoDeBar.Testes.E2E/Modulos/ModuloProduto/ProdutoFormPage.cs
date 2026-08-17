using Microsoft.Playwright;

namespace GerenciamentoDeBar.Testes.E2E.Modulos.ModuloProduto;

public class ProdutoFormPage
{
    private readonly IPage page;
    private readonly string urlBase;

    public ProdutoFormPage(IPage page, string urlBase)
    {
        this.page = page;
        this.urlBase = urlBase;
    }

    // =========================================================
    // URLS
    // =========================================================

    public string UrlCadastrar =>
        $"{urlBase}/Produto/Cadastrar";

    public string UrlEditar(Guid id) =>
        $"{urlBase}/Produto/Editar?id={id}";


    // =========================================================
    // TÍTULO
    // =========================================================

    public ILocator TituloCadastrar =>
        page.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Cadastrar Produto",
                Exact = true
            }
        );

    public ILocator TituloEditar =>
        page.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Editar Produto",
                Exact = true
            }
        );


    // =========================================================
    // CAMPOS
    // =========================================================

    public ILocator Nome =>
        page.GetByLabel("Nome");

    public ILocator Preco =>
        page.GetByLabel("Preço");



    // =========================================================
    // VALIDAÇÕES
    // =========================================================

    public ILocator ErroNome =>
        page.Locator("[data-valmsg-for='Nome']");

    public ILocator ErroPreco =>
        page.Locator("[data-valmsg-for='Preco']");

    public ILocator ResumoErros =>
        page.Locator(".validation-summary-errors");

    public ILocator ErroNomeDuplicado =>
    page.GetByText(
        "Já existe um produto com este nome.",
        new()
        {
            Exact = true
        }
    );

    public async Task DesabilitarValidacaoNativaAsync()
    {
        ILocator formulario = page
            .Locator("form")
            .Filter(new()
            {
                Has = page.GetByLabel("Preço")
            });

        await formulario.EvaluateAsync(
            @"form => form.setAttribute('novalidate', 'novalidate')"
        );
    }

    public async Task PreencherPrecoAsync(decimal preco)
    {
        await Preco.EvaluateAsync(
            @"(element, value) => {
            element.value = value;
            element.dispatchEvent(new Event('input', { bubbles: true }));
            element.dispatchEvent(new Event('change', { bubbles: true }));
        }",
            preco.ToString(System.Globalization.CultureInfo.InvariantCulture)
        );
    }


    // =========================================================
    // BOTÕES
    // =========================================================

    public ILocator Salvar =>
        page.GetByRole(
            AriaRole.Button,
            new()
            {
                Name = "Salvar"
            }
        );

    public ILocator SalvarAlteracoes =>
        page.GetByRole(
            AriaRole.Button,
            new()
            {
                Name = "Salvar Alterações"
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
    // NAVEGAÇÃO
    // =========================================================

    public async Task IrParaCadastrar()
    {
        await page.GotoAsync(UrlCadastrar);
    }

    public async Task IrParaEditar(Guid id)
    {
        await page.GotoAsync(UrlEditar(id));
    }


}
