using System;
using FluentResults;
using GerenciamentoDeBar.Aplicacao.Compartilhado;
using GerenciamentoDeBar.Dominio.Modulos.ModuloMesa;
using static GerenciamentoDeBar.Aplicacao.Modulos.ModuloMesa.MesaDtos;

namespace GerenciamentoDeBar.Aplicacao.Modulos.ModuloMesa;

public class ServicoMesa : ServicoBase<Mesa>
{
    private readonly IRepositorioMesa repositorioMesa;
    //private readonly IrepositorioConta repositorioConta;

    public ServicoMesa(IRepositorioMesa repositorioMesa
    //IrepositorioConta repositorioConta
    )
    {
        this.repositorioMesa = repositorioMesa;
        //this.repositorioConta = repositorioConta
    }

    public Result Cadastrar(CadastrarMesaDto dto)
    {
        if (ExisteMesaComMesmoNumero(dto.NumeroDaMesa))
            return Falha(nameof(dto.NumeroDaMesa), "Já existe uma mesa com este número.");

        Mesa novaMesa = new Mesa(dto.NumeroDaMesa, dto.QuantidadeDeLugares);

        Result resultadoValidacao = ValidarEntidade(novaMesa);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioMesa.Cadastrar(novaMesa);

        return Result.Ok();
    }

    public Result Editar(EditarMesaDto dto)
    {
        if (ExisteMesaComMesmoNumero(dto.NumeroDaMesa, dto.Id))
            return Falha(nameof(dto.NumeroDaMesa), "Já existe uma mesa com este número.");

        Mesa mesaAtualizada = new Mesa(dto.NumeroDaMesa, dto.QuantidadeDeLugares);

        Result resultadoValidacao = ValidarEntidade(mesaAtualizada);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar = repositorioMesa.Editar(dto.Id, mesaAtualizada);

        if (!conseguiuEditar)
            return Falha(string.Empty, "Mesa não encontrada.");

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Mesa? mesa = repositorioMesa.SelecionarPorId(id);

        if (mesa == null)
            return Falha(string.Empty, "Mesa não encontrada.");

        /*
        if (PossuiContaEmAberto(id))
            return Falha(string.Empty, "Não é possível excluir esta mesa, pois ela possui uma conta em aberto.");
        */


        repositorioMesa.Excluir(id);

        return Result.Ok();
    }

    public List<ListarMesasDto> SelecionarTodos()
    {
        return repositorioMesa
          .SelecionarTodos()
          .Select(m => new ListarMesasDto(
            m.Id,
            m.NumeroDaMesa,
            m.QuantidadeDeLugares,
            m.StatusMesa))
          .ToList();
    }

    public Result<DetalhesMesaDto> SelecionarPorId(Guid id)
    {
        Mesa? mesa = repositorioMesa.SelecionarPorId(id);

        if (mesa == null)
            return Result.Fail("Mesa não encontrada.");

        return Result.Ok(new DetalhesMesaDto(
            mesa.Id,
            mesa.NumeroDaMesa,
            mesa.QuantidadeDeLugares,
            mesa.StatusMesa));
    }

    public Result<DetalhesMesaDto> PesquisarPorNumero(int numeroDaMesa)
    {
        Mesa? mesa = repositorioMesa
            .Filtrar(m => m.NumeroDaMesa == numeroDaMesa)
            .FirstOrDefault();

        if (mesa == null)
            return Result.Fail("Mesa não encontrada.");

        return Result.Ok(new DetalhesMesaDto(
            mesa.Id,
            mesa.NumeroDaMesa,
            mesa.QuantidadeDeLugares,
            mesa.StatusMesa));
    }

    public List<ListarMesasDto> PesquisarPorStatus(StatusMesa status)
    {
        return repositorioMesa
            .Filtrar(m => m.StatusMesa == status)
            .Select(m => new ListarMesasDto(
                m.Id,
                m.NumeroDaMesa,
                m.QuantidadeDeLugares,
                m.StatusMesa))
            .ToList();
    }

    private bool ExisteMesaComMesmoNumero(int numeroDaMesa, Guid? idIgnorado = null)
    {
        return repositorioMesa.SelecionarTodos()
        .Any(m => m.Id != idIgnorado &&
        m.NumeroDaMesa == numeroDaMesa);
    }

    /*
    private bool PossuiContaEmAberto(Guid mesaId)
    {
        return repositorioConta
            .SelecionarTodos()
            .Any(c => c.Mesa.Id == mesaId &&
                     c.Status == StatusConta.Aberta);
    }
    */

}
