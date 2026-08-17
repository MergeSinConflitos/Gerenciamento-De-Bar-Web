using Microsoft.Playwright;

namespace GerenciamentoDeBar.Testes.E2E.Modulos.ModuloGarcom;

public class GarcomFormPage
{
    private readonly IPage page;
    private readonly string urlBase;

    public GarcomFormPage(IPage page, string urlBase)
    {
        this.page = page;
        this.urlBase = urlBase;
    }

    // =========================================================
    // URLS
    // =========================================================

    public string UrlCadastrar =>
        $"{urlBase}/Garcom/Cadastrar";

    public string UrlEditar(Guid id) =>
        $"{urlBase}/Garcom/Editar?id={id}";


    // =========================================================
    // TÍTULO
    // =========================================================

    public ILocator TituloCadastrar =>
        page.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Cadastrar Garçom",
                Exact = true

            }
        );

    public ILocator TituloEditar =>
        page.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Editar Garçom",
                Exact = true
            }
        );


    // =========================================================
    // CAMPOS
    // =========================================================

    public ILocator Nome =>
        page.GetByLabel("Nome");

    public ILocator Telefone =>
        page.GetByLabel("Telefone");

    public ILocator Cpf =>
        page.GetByLabel("CPF");


    // =========================================================
    // VALIDAÇÕES
    // =========================================================

    public ILocator ErroNome =>
        page.Locator("[data-valmsg-for='Nome']");

    public ILocator ErroTelefone =>
        page.Locator("[data-valmsg-for='Telefone']");

    public ILocator ErroCpf =>
        page.Locator("[data-valmsg-for='Cpf']");

    public ILocator ResumoErros =>
        page.Locator(".validation-summary-errors");


    public ILocator ErroCpfDuplicado =>
    page.GetByText(
        "Já existe um garçom com este CPF.",
        new()
        {
            Exact = true
        }
    );

    public ILocator ErroTelefoneDuplicado =>
    page.GetByText(
        "Já existe um garçom com este telefone.",
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
                Has = page.GetByLabel("CPF")
            });

        await formulario.EvaluateAsync(
            @"form => form.setAttribute('novalidate', 'novalidate')"
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

    public ILocator Cancelar =>
        page.GetByRole(
            AriaRole.Link,
            new()
            {
                Name = "Cancelar",
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
