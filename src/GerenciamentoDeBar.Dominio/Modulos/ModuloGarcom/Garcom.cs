using System;
using System.Text.RegularExpressions;
using GerenciamentoDeBar.Dominio.Compartilhado;

namespace GerenciamentoDeBar.Dominio.Modulos.ModuloGarcom;

public class Garcom : EntidadeBase<Garcom>
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Cpf { get; set; }

    public Garcom(string nome, string telefone, string cpf)
    {
        Nome = nome;
        Telefone = telefone;
        Cpf = cpf;
    }

    public override void Atualizar(Garcom entidadeAtualizada)
    {
        Garcom garcomAtualizado = entidadeAtualizada;

        Nome = garcomAtualizado.Nome;
        Telefone = garcomAtualizado.Telefone;
        Cpf = garcomAtualizado.Cpf;
    }

    public Garcom()
    {

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

        if (string.IsNullOrWhiteSpace(Telefone))
        {
            erros.Add("O campo \"Telefone\" deve ser preenchido");
        }
        else if (!Regex.IsMatch(
            Telefone,
            @"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$"))
        {
            erros.Add("O telefone informado é inválido");
        }

        if (string.IsNullOrWhiteSpace(Cpf))
        {
            erros.Add("O campo \"CPF\" deve ser preenchido");
        }
        else if (!Regex.IsMatch(
            Cpf,
            @"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$"))
        {
            erros.Add("O CPF informado é inválido");
        }

        return erros;
    }


}
