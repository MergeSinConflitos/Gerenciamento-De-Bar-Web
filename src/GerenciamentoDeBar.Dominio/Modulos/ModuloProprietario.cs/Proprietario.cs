namespace GerenciamentoDeBar.Dominio.Modulos.ModuloProprietario.cs;

//Equivalente a Instituição no escola de cursos
public sealed class Proprietario
{
    public Guid UserId { get; set; }
    public string Nome { get; set; } = string.Empty;
}