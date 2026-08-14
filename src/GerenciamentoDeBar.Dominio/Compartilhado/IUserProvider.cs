namespace GerenciamentoDeBar.Dominio.Compartilhado;

public interface IUserProvider
{
    Guid? Id { get; }
    bool EstaAutenticado { get; }
}