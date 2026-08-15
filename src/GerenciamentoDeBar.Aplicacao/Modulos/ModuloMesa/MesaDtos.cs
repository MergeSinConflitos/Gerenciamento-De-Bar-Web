using GerenciamentoDeBar.Dominio.Modulos.ModuloMesa;

namespace GerenciamentoDeBar.Aplicacao.Modulos.ModuloMesa;

public record class MesaDtos
{
    public record ListarMesasDto(
        Guid Id,
        int NumeroDaMesa,
        int QuantidadeDeLugares,
        StatusMesa StatusMesa
    );

    public record CadastrarMesaDto(
        int NumeroDaMesa,
        int QuantidadeDeLugares
    );

    public record EditarMesaDto(
        Guid Id,
        int NumeroDaMesa,
        int QuantidadeDeLugares
    );

    public record DetalhesMesaDto(
        Guid Id,
        int NumeroDaMesa,
        int QuantidadeDeLugares,
        StatusMesa StatusMesa
    );
}
