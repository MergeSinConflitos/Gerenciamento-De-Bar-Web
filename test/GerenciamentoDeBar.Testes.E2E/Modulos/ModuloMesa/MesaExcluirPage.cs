using Microsoft.Playwright;

namespace GerenciamentoDeBar.Testes.E2E.Modulos.ModuloMesa;

public class MesaExcluirPage
{
    private readonly IPage page;
    private readonly string urlBase;

    public MesaExcluirPage(
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
        $"{urlBase}/Mesa/Excluir";


    //=================================================
    // TÍTULO
    //=================================================

    public ILocator Titulo =>
        page.GetByText("Excluir Mesa");


    //=================================================
    // MENSAGEM DE CONFIRMAÇÃO
    //=================================================

    public ILocator MensagemConfirmacao =>
        page.GetByText(
            "Tem certeza que deseja excluir esta mesa?"
        );


    //=================================================
    // DADOS DA MESA
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


    //=================================================
    // BOTÕES
    //=================================================

    public ILocator Confirmar =>
        page.GetByRole(
            AriaRole.Button,
            new()
            {
                Name = "Sim, Excluir Mesa"
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
}