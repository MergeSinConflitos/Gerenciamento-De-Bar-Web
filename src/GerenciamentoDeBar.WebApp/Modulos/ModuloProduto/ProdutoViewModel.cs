
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDeBar.WebApp.Modulos.ModuloProduto;

public record ProdutoViewModel
{
    public record ListarProdutosViewModel(
        Guid Id,
        string Nome,
        decimal Preco
    );

    public record CadastrarProdutoViewModel(
        [Required(
            ErrorMessage = "O campo \"Nome\" deve ser preenchido"
        )]
        [StringLength(
            100,
            MinimumLength = 2,
            ErrorMessage = "O nome deve ter entre 2 e 100 caracteres"
        )]
        string Nome,

        [Range(
            0.01,
            double.MaxValue,
            ErrorMessage = "Informe um valor positivo"
        )]
        decimal Preco
    );

    public record EditarProdutoViewModel(
        Guid Id,

        [Required(
            ErrorMessage = "O campo \"Nome\" deve ser preenchido"
        )]
        [StringLength(
            100,
            MinimumLength = 2,
            ErrorMessage = "O nome deve ter entre 2 e 100 caracteres"
        )]
        string Nome,

        [Range(
            0.01,
            double.MaxValue,
            ErrorMessage = "Informe um valor positivo"
        )]
        decimal Preco
    );

    public record ExcluirProdutoViewModel(
        Guid Id,
        string Nome,
        decimal Preco
    );
}
