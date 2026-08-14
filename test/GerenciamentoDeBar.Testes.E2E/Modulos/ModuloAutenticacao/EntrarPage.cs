using Microsoft.Playwright;

namespace GerenciamentoDeBar.Testes.E2E.Modulos.ModuloAutenticacao;

public sealed class EntrarPage(
    IPage page,
    string urlBase
)
{
    public string Url =>
        $"{urlBase}/Autenticacao/Entrar";

    public ILocator Titulo =>
        page.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Acessar o Sistema",
            }
        );

    public ILocator Email =>
        page.GetByLabel(
            "E-mail",
            new()
            {
                Exact = true
            }
        );

    public ILocator Senha =>
        page.GetByLabel(
            "Senha",
            new()
            {
                Exact = true
            }
        );

    public ILocator BotaoEntrar =>
        page.GetByRole(
            AriaRole.Button,
            new()
            {
                Name = "Entrar",
            }
        );

    public ILocator LinkCriarConta =>
        page.GetByRole(
            AriaRole.Link,
            new()
            {
                Name = "Criar conta",
            }
        );

    public ILocator UsuarioAutenticado(string email) =>
        page.GetByRole(
            AriaRole.Button,
            new()
            {
                Name = email,
                Exact = true
            }
        );

    public async Task IrParaAsync()
    {
        await page.GotoAsync(Url);
    }

    public async Task PreencherAsync(
        string email,
        string senha
    )
    {
        await Email.FillAsync(email);
        await Senha.FillAsync(senha);
    }

    public async Task ConfirmarAsync()
    {
        await BotaoEntrar.ClickAsync();
    }
}