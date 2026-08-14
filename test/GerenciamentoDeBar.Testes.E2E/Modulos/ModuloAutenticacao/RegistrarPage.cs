using Microsoft.Playwright;

namespace GerenciamentoDeBar.Testes.E2E.Modulos.ModuloAutenticacao;

public sealed class RegistrarPage(
    IPage page,
    string urlBase
)
{
    public string Url =>
        $"{urlBase}/Autenticacao/Registrar";

    public ILocator Nome =>
        page.GetByLabel(
            "Nome do proprietário",
            new()
            {
                Exact = true
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

    public ILocator ConfirmarSenha =>
        page.GetByLabel(
            "Confirmar senha",
            new()
            {
                Exact = true
            }
        );

    public ILocator BotaoCriarConta =>
        page.GetByRole(
            AriaRole.Button,
            new()
            {
                Name = "Criar Conta",
            }
        );

    public ILocator LinkEntrar =>
        page.GetByRole(
            AriaRole.Link,
            new()
            {
                Name = "Entrar",
            }
        );

    public async Task IrParaAsync()
    {
        await page.GotoAsync(Url);
    }

    public async Task PreencherAsync(
        string nome,
        string email,
        string senha
    )
    {
        await Nome.FillAsync(nome);
        await Email.FillAsync(email);
        await Senha.FillAsync(senha);
        await ConfirmarSenha.FillAsync(senha);
    }

    public async Task ConfirmarAsync()
    {
        await BotaoCriarConta.ClickAsync();
    }
}