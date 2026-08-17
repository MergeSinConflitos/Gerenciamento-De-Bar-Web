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
        else if (!CpfValido(Cpf))
        {
            erros.Add("O CPF informado é inválido");
        }

        return erros;
    }

    private bool CpfValido(string cpf)
    {
        string numeros = Regex.Replace(cpf, @"\D", "");

        if (numeros.Length != 11)
            return false;

        if (numeros.Distinct().Count() == 1)
            return false;

        int soma = 0;

        for (int i = 0; i < 9; i++)
        {
            soma += int.Parse(numeros[i].ToString()) * (10 - i);
        }

        int resto = soma % 11;

        int primeiroDigito = resto < 2 ? 0 : 11 - resto;

        if (int.Parse(numeros[9].ToString()) != primeiroDigito)
            return false;

        soma = 0;

        for (int i = 0; i < 10; i++)
        {
            soma += int.Parse(numeros[i].ToString()) * (11 - i);
        }

        resto = soma % 11;

        int segundoDigito = resto < 2 ? 0 : 11 - resto;

        return int.Parse(numeros[10].ToString()) == segundoDigito;
    }
}
