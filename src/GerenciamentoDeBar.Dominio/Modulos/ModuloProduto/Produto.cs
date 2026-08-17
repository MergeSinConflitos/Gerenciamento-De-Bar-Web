using System;
using GerenciamentoDeBar.Dominio.Compartilhado;

namespace GerenciamentoDeBar.Dominio.Modulos.ModuloProduto;

public class Produto : EntidadeBase<Produto>
{

    public string Nome { get; set; }
    public decimal Preco { get; set; }

    public Produto(string nome, decimal preco)
    {
        Nome = nome;
        Preco = preco;
    }

    public Produto()
    {

    }

    public override void Atualizar(Produto entidadeAtualizada)
    {
        Produto produtoAtualizado = entidadeAtualizada;

        Nome = produtoAtualizado.Nome;
        Preco = produtoAtualizado.Preco;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
        {
            erros.Add("O campo \"Nome\" deve ser preenchido");
        }
        else if (Nome.Length < 2 || Nome.Length > 100)
        {
            erros.Add("O nome deve ter entre 2 e 100 caracteres");
        }

        if (Preco <= 0)
        {
            erros.Add("Informe um valor positivo");
        }

        return erros;
    }
}
