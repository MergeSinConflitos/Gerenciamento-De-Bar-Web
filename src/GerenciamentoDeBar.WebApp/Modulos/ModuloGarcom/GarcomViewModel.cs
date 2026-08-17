using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDeBar.WebApp.Modulos.ModuloGarcom;

public record GarcomViewModel
{
    public record ListarGarconsViewModel(
    Guid Id,
    string Nome,
    string Telefone,
    string Cpf
);

    public record CadastrarGarcomViewModel(
        [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido")]
    [StringLength(
        100,
        MinimumLength = 2,
        ErrorMessage = "O nome deve ter entre 2 e 100 caracteres"
    )]
    string Nome,

        [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido")]
    [RegularExpression(
        @"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$",
        ErrorMessage = "O telefone informado é inválido"
    )]
    string Telefone,

        [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido")]
    [RegularExpression(
        @"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$",
        ErrorMessage = "O CPF informado é inválido"
    )]
    string Cpf
    );

    public record EditarGarcomViewModel(
        Guid Id,

        [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido")]
    [StringLength(
        100,
        MinimumLength = 2,
        ErrorMessage = "O nome deve ter entre 2 e 100 caracteres"
    )]
    string Nome,

        [Required(ErrorMessage = "O campo \"Telefone\" deve ser preenchido")]
    [RegularExpression(
        @"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$",
        ErrorMessage = "O telefone informado é inválido"
    )]
    string Telefone,

        [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido")]
    [RegularExpression(
        @"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$",
        ErrorMessage = "O CPF informado é inválido"
    )]
    string Cpf
    );

    public record ExcluirGarcomViewModel(
        Guid Id,
        string Nome,
        string Telefone,
        string Cpf
    );
}
