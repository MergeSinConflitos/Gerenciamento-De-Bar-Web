using Microsoft.Playwright;

namespace GerenciamentoDeBar.Testes.E2E.Modulos.ModuloMesa;

public class MesaExcluirPage
{
    private readonly IPage page;
    private readonly string urlBase;

    public string Url =>
        $"{urlBase}/Mesa/Excluir";

    public ILocator Titulo =>
        page.GetByText("Excluir Mesa");

    public ILocator MensagemConfirmacao =>
        page.GetByText(
            "Tem certeza que deseja excluir esta mesa?"
        );

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

    public MesaExcluirPage(
        IPage page,
        string urlBase)
    {
        this.page = page;
        this.urlBase = urlBase;
    }

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

    public async Task ConfirmarAsync()
    {
        await Confirmar.ClickAsync();
    }

    public async Task VoltarAsync()
    {
        await Voltar.ClickAsync();
    }
}
