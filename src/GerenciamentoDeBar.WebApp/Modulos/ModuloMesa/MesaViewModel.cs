using System;
using System.ComponentModel.DataAnnotations;
using GerenciamentoDeBar.Dominio.Modulos.ModuloMesa;

namespace GerenciamentoDeBar.WebApp.Modulos.ModuloMesa;

public record MesaViewModel
{
    public record ListarMesasViewModel(
        Guid Id,
        int NumeroDaMesa,
        int QuantidadeDeLugares,
        StatusMesa StatusMesa
    );

    public record CadastrarMesaViewModel(
        [Required]
        [Range(1, int.MaxValue,ErrorMessage ="Informe um número positivo")]
        int NumeroDaMesa,

        [Required(ErrorMessage = "Informe um número positivo")]
        [Range(1,10,ErrorMessage ="A quantidade maxima é de 10 lugares")]
        int QuantidadeDeLugares
    );

    public record EditarMesaViewModel(
        Guid Id,

        [Required]
        [Range(1, int.MaxValue,ErrorMessage ="Informe um número positivo")]
        int NumeroDaMesa,

        [Required(ErrorMessage = "Informe um número positivo")]
        [Range(1,10,ErrorMessage ="A quantidade maxima é de 10 lugares")]
        int QuantidadeDeLugares
    );

    public record ExcluirMesaViewModel(
        Guid Id,
        int NumeroDaMesa,
        int QuantidadeDeLugares,
        StatusMesa StatusMesa
    );
}
