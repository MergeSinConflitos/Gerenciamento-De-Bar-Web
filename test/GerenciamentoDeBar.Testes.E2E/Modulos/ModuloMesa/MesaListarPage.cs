using Microsoft.Playwright;

namespace GerenciamentoDeBar.Testes.E2E.Modulos.ModuloMesa;

public class MesaListarPage
{
    private readonly IPage page;
    private readonly string urlBase;

    public MesaListarPage(IPage page, string urlBase)
    {
        this.page = page;
        this.urlBase = urlBase;
    }

    public string Url => $"{urlBase}/Mesa/Listar";


    //=================================================
    // CABEÇALHO
    //=================================================

    public ILocator Titulo =>
        page.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Listagem de Mesas"
            }
        );

    public ILocator CadastrarNovo =>
        page.GetByText("Cadastrar Nova Mesa");


    //=================================================
    // MENSAGENS
    //=================================================

    public ILocator EstadoVazio =>
        page.GetByText("Nenhuma mesa cadastrada.");

    public ILocator MensagemErro =>
        page.Locator(".alert-danger");


    //=================================================
    // FILTROS
    //=================================================

    public ILocator NumeroDaMesa(int numero)
    {
        return page.GetByText(
            numero.ToString(),
            new()
            {
                Exact = true
            }
        );
    }

    public ILocator StatusDaMesa(
        int numeroDaMesa,
        string status)
    {
        ILocator mesa =
            NumeroDaMesa(numeroDaMesa);

        ILocator card =
            mesa.Locator(
                "xpath=ancestor::div[contains(@class,'card')][1]"
            );

        return card
            .Locator(".badge")
            .GetByText(
                status,
                new()
                {
                    Exact = true
                }
            );
    }


    //=================================================
    // NAVEGAÇÃO
    //=================================================

    public async Task IrParaAsync()
    {
        await page.GotoAsync(Url);
    }

    public async Task CadastrarAsync()
    {
        await CadastrarNovo.ClickAsync();
    }


    //=================================================
    // AÇÕES
    //=================================================

    public async Task EditarAsync(int numero)
    {
        ILocator mesa =
            NumeroDaMesa(numero);

        ILocator card =
            mesa.Locator(
                "xpath=ancestor::div[contains(@class,'card')][1]"
            );

        await card.GetByRole(
            AriaRole.Link,
            new()
            {
                Name = "Editar"
            }
        ).ClickAsync();
    }

    public async Task ExcluirAsync(int numero)
    {
        ILocator mesa =
            NumeroDaMesa(numero);

        ILocator card =
            mesa.Locator(
                "xpath=ancestor::div[contains(@class,'card')][1]"
            );

        await card.GetByRole(
            AriaRole.Link,
            new()
            {
                Name = "Excluir"
            }
        ).ClickAsync();
    }


    //=================================================
    // PESQUISA
    //=================================================

    public async Task PesquisarPorNumeroAsync(int numero)
    {
        ILocator campoNumero =
            page.GetByLabel("Número da Mesa");

        await campoNumero.FillAsync(
            numero.ToString()
        );

        await page.GetByRole(
            AriaRole.Button,
            new()
            {
                Name = "Pesquisar"
            }
        ).ClickAsync();
    }

    public async Task PesquisarPorStatusAsync(string status)
    {
        ILocator campoStatus =
            page.GetByLabel("Status");

        await campoStatus.SelectOptionAsync(
            new SelectOptionValue
            {
                Label = status
            }
        );

        await page.GetByRole(
            AriaRole.Button,
            new()
            {
                Name = "Pesquisar"
            }
        ).ClickAsync();
    }

    public async Task LimparFiltrosAsync()
    {
        await page.GetByTitle("Limpar filtros")
            .ClickAsync();
    }
}