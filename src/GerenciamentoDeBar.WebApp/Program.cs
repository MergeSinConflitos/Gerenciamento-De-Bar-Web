
using GerenciamentoDeBar.Aplicacao;
using GerenciamentoDeBar.Infra.Compartilhado;
using GerenciamentoDeBar.WebApp.Compartilhado;

var builder = WebApplication.CreateBuilder(args);

//========================================================================================
// LEMBRETES

// * REVER MODULO DE MESA APÓS IMPLEMENTAÇÃO DO MODULO DE CONTA(DESENVOLVIMENTO E TESTES)
// * REVER MODULO DE GARÇOM APÓS IMPLEMENTAÇÃO DO MODULO DECONTA(DESENVOLVIMENTO E TESTES)

//========================================================================================


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