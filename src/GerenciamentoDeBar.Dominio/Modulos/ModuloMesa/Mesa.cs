using System;
using GerenciamentoDeBar.Dominio.Compartilhado;

namespace GerenciamentoDeBar.Dominio.Modulos.ModuloMesa;

public class Mesa : EntidadeBase<Mesa>
{
    public int NumeroDaMesa { get; set; }
    public int QuantidadeDeLugares { get; set; }
    public StatusMesa StatusMesa { get; set; } = StatusMesa.Livre;

    public Mesa(int numeroDaMesa, int quantidadeDeLugares)
    {
        NumeroDaMesa = numeroDaMesa;
        QuantidadeDeLugares = quantidadeDeLugares;
    }

    public Mesa()
    {

    }

    public override void Atualizar(Mesa entidadeAtualizada)
    {
        Mesa? mesaAtualizada = entidadeAtualizada;

        NumeroDaMesa = mesaAtualizada.NumeroDaMesa;
        QuantidadeDeLugares = mesaAtualizada.QuantidadeDeLugares;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (NumeroDaMesa <= 0)
        {
            erros.Add("Informe um número positivo");
        }

        if (QuantidadeDeLugares <= 0)
        {
            erros.Add("Informe um número positivo");
        }

        if (QuantidadeDeLugares > 10)
        {
            erros.Add("A quantidade máxima é de 10 lugares");
        }

        return erros;
    }
}
