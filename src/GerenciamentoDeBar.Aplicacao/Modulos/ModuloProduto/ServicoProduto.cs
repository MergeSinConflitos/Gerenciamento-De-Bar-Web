using FluentResults;
using GerenciamentoDeBar.Aplicacao.Compartilhado;
using GerenciamentoDeBar.Dominio.Modulos.ModuloProduto;
using static GerenciamentoDeBar.Aplicacao.Modulos.ModuloProduto.ProdutoDtos;

namespace GerenciamentoDeBar.Aplicacao.Modulos.ModuloProduto;

public class ServicoProduto : ServicoBase<Produto>
{
    private readonly IRepositorioProduto repositorioProduto;

    // private readonly IRepositorioItemPedido repositorioItemPedido;

    public ServicoProduto(
        IRepositorioProduto repositorioProduto
    // IRepositorioItemPedido repositorioItemPedido
    )
    {
        this.repositorioProduto = repositorioProduto;

        // this.repositorioItemPedido = repositorioItemPedido;
    }

    public Result Cadastrar(CadastrarProdutoDto dto)
    {
        if (ExisteProdutoComNome(dto.Nome))
            return Falha(
                string.Empty,
                "Já existe um produto com este nome."
            );

        Produto novoProduto = new(
            dto.Nome,
            dto.Preco
        );

        Result resultadoValidacao =
            ValidarEntidade(novoProduto);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        repositorioProduto.Cadastrar(novoProduto);

        return Result.Ok();
    }

    public Result Editar(EditarProdutoDto dto)
    {
        if (ExisteProdutoComNome(dto.Nome, dto.Id))
            return Falha(
                string.Empty,
                "Já existe um produto com este nome."
            );

        Produto produtoAtualizado = new(
            dto.Nome,
            dto.Preco
        );

        Result resultadoValidacao =
            ValidarEntidade(produtoAtualizado);

        if (resultadoValidacao.IsFailed)
            return resultadoValidacao;

        bool conseguiuEditar =
            repositorioProduto.Editar(
                dto.Id,
                produtoAtualizado
            );

        if (!conseguiuEditar)
            return Falha(
                string.Empty,
                "Produto não encontrado."
            );

        return Result.Ok();
    }

    public Result Excluir(Guid id)
    {
        Produto? produto =
            repositorioProduto.SelecionarPorId(id);

        if (produto == null)
            return Falha(
                string.Empty,
                "Produto não encontrado."
            );

        /*
        if (PossuiItensPedidoAssociados(id))
            return Falha(
                string.Empty,
                "Não é possível excluir este produto, pois ele possui itens de pedido associados."
            );
        */

        repositorioProduto.Excluir(id);

        return Result.Ok();
    }

    public List<ListarProdutosDto> SelecionarTodos()
    {
        return repositorioProduto
            .SelecionarTodos()
            .Select(p => new ListarProdutosDto(
                p.Id,
                p.Nome,
                p.Preco
            ))
            .ToList();
    }

    public Result<DetalhesProdutoDto> SelecionarPorId(Guid id)
    {
        Produto? produto =
            repositorioProduto.SelecionarPorId(id);

        if (produto == null)
            return Result.Fail(
                "Produto não encontrado."
            );

        return Result.Ok(
            new DetalhesProdutoDto(
                produto.Id,
                produto.Nome,
                produto.Preco
            )
        );
    }

    public List<ListarProdutosDto> PesquisarPorNome(string nome)
    {
        return repositorioProduto
            .Filtrar(p =>
                p.Nome.Contains(
                    nome,
                    StringComparison.OrdinalIgnoreCase
                ))
            .Select(p => new ListarProdutosDto(
                p.Id,
                p.Nome,
                p.Preco
            ))
            .ToList();
    }


    /*
    public List<ListarItensPedidoDto> SelecionarItensPedidoDoProduto(
        Guid produtoId
    )
    {
        return repositorioItemPedido
            .SelecionarTodos()
            .Where(i => i.Produto.Id == produtoId)
            .Select(i => new ListarItensPedidoDto(
                i.Id,
                i.Quantidade,
                i.PrecoUnitario
            ))
            .ToList();
    }
    */

    private bool ExisteProdutoComNome(string nome, Guid? idIgnorar = null)
    {
        return repositorioProduto
            .SelecionarTodos()
            .Any(p =>
                p.Nome.Equals(
                    nome,
                    StringComparison.OrdinalIgnoreCase
                )
                && (idIgnorar == null || p.Id != idIgnorar)
            );
    }

    /*
    private bool PossuiItensPedidoAssociados(Guid produtoId)
    {
        return repositorioItemPedido
            .SelecionarTodos()
            .Any(i =>
                i.Produto.Id == produtoId);
    }
    */
}