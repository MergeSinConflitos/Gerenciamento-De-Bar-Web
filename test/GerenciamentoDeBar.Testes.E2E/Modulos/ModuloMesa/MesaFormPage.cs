using Microsoft.Playwright;

namespace GerenciamentoDeBar.Testes.E2E.Modulos.ModuloMesa;

public class MesaFormPage
{
    private readonly IPage page;
    private readonly string urlBase;

    public MesaFormPage(
        IPage page,
        string urlBase)
    {
        this.page = page;
        this.urlBase = urlBase;
    }


    //=================================================
    // URL
    //=================================================

    public string Url =>
        $"{urlBase}/Mesa/Cadastrar";


    //=================================================
    // CAMPOS
    //=================================================

    public ILocator NumeroDaMesa =>
        page.GetByLabel("Número da Mesa");

    public ILocator QuantidadeDeLugares =>
        page.GetByLabel("Quantidade de Lugares");


    //=================================================
    // BOTÕES
    //=================================================

    public ILocator Confirmar =>
        page.GetByRole(
            AriaRole.Button,
            new()
            {
                Name = "Salvar"
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


    //=================================================
    // VALIDAÇÕES
    //=================================================

    public ILocator ErroNumeroDaMesa =>
        page.GetByText(
            "Informe um número inteiro positivo.",
            new()
            {
                Exact = true
            }
        );

    public ILocator ErroQuantidadeDeLugares =>
        page.GetByText(
            "A mesa pode possuir de 1 a 10 lugares.",
            new()
            {
                Exact = true
            }
        );

    public ILocator ErroNumeroDaMesaDuplicado =>
        page.GetByText(
            "Já existe uma mesa com este número.",
            new()
            {
                Exact = true
            }
        );


    //=================================================
    // NAVEGAÇÃO
    //=================================================

    public async Task IrParaCadastroAsync()
    {
        await page.GotoAsync(Url);
    }


    //=================================================
    // PREENCHIMENTO
    //=================================================

    public async Task PreencherAsync(
        int numeroDaMesa,
        int quantidadeDeLugares)
    {
        await NumeroDaMesa.FillAsync(
            numeroDaMesa.ToString()
        );

        await QuantidadeDeLugares.FillAsync(
            quantidadeDeLugares.ToString()
        );
    }


    //=================================================
    // AÇÕES
    //=================================================

    public async Task ConfirmarAsync()
    {
        await Confirmar.ClickAsync();
    }

    public async Task VoltarAsync()
    {
        await Voltar.ClickAsync();
    }


    //=================================================
    // VALIDAÇÃO NATIVA
    //=================================================

    public async Task DesabilitarValidacaoNativaAsync()
    {
        await page.Locator("form")
            .Filter(new()
            {
                Has = page.Locator(
                    "input[name='NumeroDaMesa']"
                )
            })
            .EvaluateAsync(
                "form => form.noValidate = true"
            );
    }
}