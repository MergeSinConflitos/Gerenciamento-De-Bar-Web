using GerenciamentoDeBar.Dominio.Compartilhado;

namespace GerenciamentoDeBar.Testes.Integraçao.Compartilhado.Identity;

public sealed class ProvedorDeUsuarioFake(Guid userId) : IUserProvider
{
    public Guid? Id => userId;

    public bool EstaAutenticado => true;
}