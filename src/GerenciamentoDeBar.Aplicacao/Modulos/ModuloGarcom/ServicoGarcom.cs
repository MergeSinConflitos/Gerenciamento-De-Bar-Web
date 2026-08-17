using FluentResults;
using GerenciamentoDeBar.Aplicacao.Compartilhado;
using GerenciamentoDeBar.Dominio.Modulos.ModuloGarcom;
using static GerenciamentoDeBar.Aplicacao.Modulos.ModuloGarcom.GarcomDtos;

namespace GerenciamentoDeBar.Aplicacao.Modulos.ModuloGarcom;

public class ServicoGarcom : ServicoBase<Garcom>
{
    private readonly IRepositorioGarcom repositorioGarcom;

    // private readonly IRepositorioConta repositorioConta;

    public ServicoGarcom(
        IRepositorioGarcom repositorioGarcom
    // IRepositorioConta repositorioConta
    )
    {
        this.repositorioGarcom = repositorioGarcom;

        // this.repositorioConta = repositorioConta;
    }

    public Result Cadastrar(CadastrarGarcomDto dto)
    {
        if (ExisteGarcomComMesmoCpf(dto.Cpf))
            return Falha(
                nameof(dto.Cpf),
                "Já existe um garçom com este CPF."
            );

        if (ExisteGarcomComMesmoTelefone(dto.Telefone))
            return Falha(
                nameof(dto.Telefone),
                "Já existe um garçom com este telefone."
            );

        Garcom novoGarcom = new(
            dto.Nome,
            dto.Telefone,
            dto.Cpf
        );

        Result resultadoValidacao =
            ValidarEntidade(novoGarcom);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioGarcom.Cadastrar(novoGarcom);

        return Result.Ok();
    }

    public Result Editar(EditarGarcomDto dto)
    {
        if (ExisteGarcomComMesmoCpf(dto.Cpf, dto.Id))
            return Falha(
                nameof(dto.Cpf),
                "Já existe um garçom com este CPF."
            );

        if (ExisteGarcomComMesmoTelefone(dto.Telefone, dto.Id))
            return Falha(
                nameof(dto.Telefone),
                "Já existe um garçom com este telefone."
            );

        Garcom garcomAtualizado = new(
            dto.Nome,
            dto.Telefone,
            dto.Cpf
        );

        Result resultadoValidacao =
            ValidarEntidade(garcomAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar =
            repositorioGarcom.Editar(
                dto.Id,
                garcomAtualizado
            );

        if (!conseguiuEditar)
            return Falha(
                string.Empty,
                "Garçom não encontrado."
            );

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Garcom? garcom =
            repositorioGarcom.SelecionarPorId(id);

        if (garcom == null)
            return Falha(
                string.Empty,
                "Garçom não encontrado."
            );

        /*
        if (PossuiContasAssociadas(id))
            return Falha(
                string.Empty,
                "Não é possível excluir este garçom, pois ele possui contas associadas."
            );
        */

        repositorioGarcom.Excluir(id);

        return Result.Ok();
    }

    public List<ListarGarconsDto> SelecionarTodos()
    {
        return repositorioGarcom
            .SelecionarTodos()
            .Select(g => new ListarGarconsDto(
                g.Id,
                g.Nome,
                g.Telefone,
                g.Cpf
            ))
            .ToList();
    }

    public Result<DetalhesGarcomDto> SelecionarPorId(Guid id)
    {
        Garcom? garcom =
            repositorioGarcom.SelecionarPorId(id);

        if (garcom == null)
            return Result.Fail(
                "Garçom não encontrado."
            );

        return Result.Ok(
            new DetalhesGarcomDto(
                garcom.Id,
                garcom.Nome,
                garcom.Telefone,
                garcom.Cpf
            )
        );
    }

    public List<ListarGarconsDto> PesquisarPorNome(string nome)
    {
        return repositorioGarcom
            .Filtrar(g =>
                g.Nome.Contains(
                    nome,
                    StringComparison.OrdinalIgnoreCase
                ))
            .Select(g => new ListarGarconsDto(
                g.Id,
                g.Nome,
                g.Telefone,
                g.Cpf
            ))
            .ToList();
    }

    /*
    public List<ListarContasDto> SelecionarContasDoGarcom(Guid garcomId)
    {
        return repositorioConta
            .SelecionarTodos()
            .Where(c => c.Garcom.Id == garcomId)
            .Select(c => new ListarContasDto(
                c.Id,
                c.NomeCliente,
                c.DataAbertura,
                c.Status
            ))
            .ToList();
    }
    */

    private bool ExisteGarcomComMesmoCpf(
        string cpf,
        Guid? idIgnorado = null
    )
    {
        return repositorioGarcom
            .SelecionarTodos()
            .Any(g =>
                g.Id != idIgnorado &&
                g.Cpf == cpf
            );
    }

    private bool ExisteGarcomComMesmoTelefone(
        string telefone,
        Guid? idIgnorado = null
    )
    {
        return repositorioGarcom
            .SelecionarTodos()
            .Any(g =>
                g.Id != idIgnorado &&
                g.Telefone == telefone
            );
    }

    /*
    private bool PossuiContasAssociadas(Guid garcomId)
    {
        return repositorioConta
            .SelecionarTodos()
            .Any(c =>
                c.Garcom.Id == garcomId);
    }
    */
}