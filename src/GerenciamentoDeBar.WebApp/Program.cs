
using GerenciamentoDeBar.Aplicacao;
using GerenciamentoDeBar.Aplicacao.Compartilhado;
using GerenciamentoDeBar.Infra.Compartilhado;
using GerenciamentoDeBar.WebApp.Compartilhado;

var builder = WebApplication.CreateBuilder(args);


// =========================================================
// CONFIGURAÇÃO DOS SERVIÇOS
// =========================================================

// Infraestrutura
builder.Services.AddInfraRepositories(
    builder.Configuration,
    builder.Logging,
    builder.Environment
);

// Aplicação
builder.Services.AddApplicationServices(
    builder.Configuration
);

// Apresentação
builder.Services.AddPresentationConfig(
    builder.Configuration
);


var app = builder.Build();


// =========================================================
// PIPELINE DA APLICAÇÃO
// =========================================================

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


// =========================================================
// ROTAS MVC
// =========================================================

app.MapDefaultControllerRoute();


// =========================================================
// EXECUÇÃO
// =========================================================

app.Run();