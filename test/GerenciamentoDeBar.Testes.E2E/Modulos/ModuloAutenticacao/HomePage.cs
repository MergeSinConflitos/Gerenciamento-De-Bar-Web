using Microsoft.Playwright;

namespace GerenciamentoDeBar.Testes.E2E.Modulos.ModuloHome;

public sealed class HomePage(
    IPage page,
    string urlBase
)
{
    public string Url =>
        $"{urlBase}/";

    public ILocator Titulo =>
     page.GetByRole(
         AriaRole.Heading,
         new()
         {
             Name = "Gerenciamento de Bar"
         }
     );
    public ILocator LinkEntrar =>
        page.Locator("a[href='/Autenticacao/Entrar']")
            .First;

    public ILocator BotaoEntrarNoSistema =>
        page.Locator("a[href='/Autenticacao/Entrar']")
            .Filter(
                new()
                {
                    HasText = "Entrar no Sistema"
                }
            );

    public ILocator ModuloMesas =>
        page.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Mesas",
                Exact = true
            }
        );

    public ILocator ModuloGarcons =>
        page.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Garçons",
                Exact = true
            }
        );

    public ILocator ModuloProdutos =>
        page.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Produtos",
                Exact = true
            }
        );

    public ILocator ModuloContas =>
        page.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Contas",
                Exact = true
            }
        );

    public ILocator ModuloPedidos =>
        page.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Pedidos",
                Exact = true
            }
        );

    public async Task IrParaAsync()
    {
        await page.GotoAsync(Url);
    }
}