
using FluentResults;
using GerenciamentoDeBar.Aplicacao.Modulos.ModuloProduto;
using GerenciamentoDeBar.Dominio.Modulos.ModuloProduto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using static GerenciamentoDeBar.Aplicacao.Modulos.ModuloProduto.ProdutoDtos;

namespace GerenciamentoDeBar.Testes.Unidade.Modulos.ModuloProduto;

[TestClass]
public class ServicoProdutoTests
{
    [TestMethod]
    public void Cadastrar_ComDadosValidos_PersisteProduto()
    {
        // Arranjo
        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto
            .Setup(r => r.SelecionarTodos())
            .Returns([]);

        Produto? produtoCadastrado = null;

        repositorioProduto
            .Setup(r => r.Cadastrar(It.IsAny<Produto>()))
            .Callback<Produto>(produto =>
            {
                produtoCadastrado = produto;
            });

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        // Ação
        Result resultado = servicoProduto.Cadastrar(
            new CadastrarProdutoDto(
                "Cerveja",
                10.50m
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsSuccess);

        Assert.IsNotNull(produtoCadastrado);

        Assert.AreEqual(
            "Cerveja",
            produtoCadastrado.Nome
        );

        Assert.AreEqual(
            10.50m,
            produtoCadastrado.Preco
        );

        repositorioProduto.Verify(
            r => r.Cadastrar(It.IsAny<Produto>()),
            Times.Once
        );
    }


    // =========================================================
    // VALIDAÇÃO - NOME
    // =========================================================

    [TestMethod]
    public void Cadastrar_ComNomeVazio_NaoPersisteProduto()
    {
        // Arranjo
        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto
            .Setup(r => r.SelecionarTodos())
            .Returns([]);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        // Ação
        Result resultado = servicoProduto.Cadastrar(
            new CadastrarProdutoDto(
                "",
                10.50m
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "O campo \"Nome\" deve ser preenchido",
            resultado.Errors.Single().Message
        );

        repositorioProduto.Verify(
            r => r.Cadastrar(It.IsAny<Produto>()),
            Times.Never
        );
    }


    [TestMethod]
    public void Cadastrar_ComNomeMuitoCurto_NaoPersisteProduto()
    {
        // Arranjo
        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto
            .Setup(r => r.SelecionarTodos())
            .Returns([]);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        // Ação
        Result resultado = servicoProduto.Cadastrar(
            new CadastrarProdutoDto(
                "A",
                10.50m
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "O nome deve ter entre 2 e 100 caracteres",
            resultado.Errors.Single().Message
        );

        repositorioProduto.Verify(
            r => r.Cadastrar(It.IsAny<Produto>()),
            Times.Never
        );
    }


    [TestMethod]
    public void Cadastrar_ComNomeMuitoLongo_NaoPersisteProduto()
    {
        // Arranjo
        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto
            .Setup(r => r.SelecionarTodos())
            .Returns([]);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        string nome = new string('A', 101);

        // Ação
        Result resultado = servicoProduto.Cadastrar(
            new CadastrarProdutoDto(
                nome,
                10.50m
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "O nome deve ter entre 2 e 100 caracteres",
            resultado.Errors.Single().Message
        );

        repositorioProduto.Verify(
            r => r.Cadastrar(It.IsAny<Produto>()),
            Times.Never
        );
    }


    // =========================================================
    // VALIDAÇÃO - PREÇO
    // =========================================================

    [TestMethod]
    public void Cadastrar_ComPrecoZero_NaoPersisteProduto()
    {
        // Arranjo
        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto
            .Setup(r => r.SelecionarTodos())
            .Returns([]);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        // Ação
        Result resultado = servicoProduto.Cadastrar(
            new CadastrarProdutoDto(
                "Cerveja",
                0
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Informe um valor positivo",
            resultado.Errors.Single().Message
        );

        repositorioProduto.Verify(
            r => r.Cadastrar(It.IsAny<Produto>()),
            Times.Never
        );
    }


    [TestMethod]
    public void Cadastrar_ComPrecoNegativo_NaoPersisteProduto()
    {
        // Arranjo
        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto
            .Setup(r => r.SelecionarTodos())
            .Returns([]);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        // Ação
        Result resultado = servicoProduto.Cadastrar(
            new CadastrarProdutoDto(
                "Cerveja",
                -10.50m
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Informe um valor positivo",
            resultado.Errors.Single().Message
        );

        repositorioProduto.Verify(
            r => r.Cadastrar(It.IsAny<Produto>()),
            Times.Never
        );
    }

    [TestMethod]
    public void Cadastrar_ComNomeDuplicado_NaoPersisteProduto()
    {
        // Arranjo
        Mock<IRepositorioProduto> repositorioProduto = new();

        Produto produtoExistente = new(
            "Cerveja",
            8.50m
        );

        repositorioProduto
            .Setup(r => r.SelecionarTodos())
            .Returns([produtoExistente]);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        // Ação
        Result resultado = servicoProduto.Cadastrar(
            new CadastrarProdutoDto(
                "Cerveja",
                10.50m
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Já existe um produto com este nome.",
            resultado.Errors.Single().Message
        );

        repositorioProduto.Verify(
            r => r.Cadastrar(It.IsAny<Produto>()),
            Times.Never
        );
    }


    // =========================================================
    // EDIÇÃO
    // =========================================================

    [TestMethod]
    public void Editar_ComDadosValidos_PersisteProduto()
    {
        // Arranjo
        Produto produtoExistente = new(
            "Cerveja",
            10.50m
        );

        Produto? produtoAtualizado = null;

        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto
       .Setup(r => r.SelecionarTodos())
       .Returns([]);

        repositorioProduto
            .Setup(r => r.Editar(
                produtoExistente.Id,
                It.IsAny<Produto>()
            ))
            .Callback<Guid, Produto>((id, produto) =>
            {
                produtoAtualizado = produto;
            })
            .Returns(true);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        // Ação
        Result resultado = servicoProduto.Editar(
            new EditarProdutoDto(
                produtoExistente.Id,
                "Refrigerante",
                8.50m
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsSuccess);

        Assert.IsNotNull(produtoAtualizado);

        Assert.AreEqual(
            "Refrigerante",
            produtoAtualizado.Nome
        );

        Assert.AreEqual(
            8.50m,
            produtoAtualizado.Preco
        );

        repositorioProduto.Verify(
            r => r.Editar(
                produtoExistente.Id,
                It.IsAny<Produto>()
            ),
            Times.Once
        );
    }


    [TestMethod]
    public void Editar_ComNomeVazio_NaoPersisteProduto()
    {
        // Arranjo
        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto
       .Setup(r => r.SelecionarTodos())
       .Returns([]);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        Guid id = Guid.NewGuid();

        // Ação
        Result resultado = servicoProduto.Editar(
            new EditarProdutoDto(
                id,
                "",
                10.50m
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "O campo \"Nome\" deve ser preenchido",
            resultado.Errors.Single().Message
        );

        repositorioProduto.Verify(
            r => r.Editar(
                It.IsAny<Guid>(),
                It.IsAny<Produto>()
            ),
            Times.Never
        );
    }


    [TestMethod]
    public void Editar_ComPrecoZero_NaoPersisteProduto()
    {
        // Arranjo
        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto
       .Setup(r => r.SelecionarTodos())
       .Returns([]);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        Guid id = Guid.NewGuid();

        // Ação
        Result resultado = servicoProduto.Editar(
            new EditarProdutoDto(
                id,
                "Cerveja",
                0
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Informe um valor positivo",
            resultado.Errors.Single().Message
        );

        repositorioProduto.Verify(
            r => r.Editar(
                It.IsAny<Guid>(),
                It.IsAny<Produto>()
            ),
            Times.Never
        );
    }


    [TestMethod]
    public void Editar_ProdutoNaoEncontrado_RetornaErro()
    {
        // Arranjo
        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto
       .Setup(r => r.SelecionarTodos())
       .Returns([]);

        repositorioProduto
            .Setup(r => r.Editar(
                It.IsAny<Guid>(),
                It.IsAny<Produto>()
            ))
            .Returns(false);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        Guid id = Guid.NewGuid();

        // Ação
        Result resultado = servicoProduto.Editar(
            new EditarProdutoDto(
                id,
                "Cerveja",
                10.50m
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Produto não encontrado.",
            resultado.Errors.Single().Message
        );
    }

    [TestMethod]
    public void Editar_ComNomeDuplicado_NaoPersisteProduto()
    {
        // Arranjo
        Mock<IRepositorioProduto> repositorioProduto = new();

        Produto produtoExistente = new(
            "Cerveja",
            8.50m
        );

        Produto produtoParaEditar = new(
            "Refrigerante",
            6.50m
        );

        repositorioProduto
            .Setup(r => r.SelecionarTodos())
            .Returns([
                produtoExistente,
            produtoParaEditar
            ]);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        // Ação
        Result resultado = servicoProduto.Editar(
            new EditarProdutoDto(
                produtoParaEditar.Id,
                "Cerveja",
                10.50m
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Já existe um produto com este nome.",
            resultado.Errors.Single().Message
        );

        repositorioProduto.Verify(
            r => r.Editar(
                It.IsAny<Guid>(),
                It.IsAny<Produto>()
            ),
            Times.Never
        );
    }

    // =========================================================
    // EXCLUSÃO
    // =========================================================

    [TestMethod]
    public void Excluir_ProdutoSemItensPedidoVinculados_ExcluiProduto()
    {
        // Arranjo
        Produto produto = new(
            "Cerveja",
            10.50m
        );

        Mock<IRepositorioProduto> repositorioProduto = new();

        // Mock<IRepositorioItemPedido> repositorioItemPedido = new();

        repositorioProduto
            .Setup(r => r.SelecionarPorId(produto.Id))
            .Returns(produto);

        /*
        // Quando o módulo ItemPedido existir:

        repositorioItemPedido
            .Setup(r => r.SelecionarTodos())
            .Returns([]);
        */

        repositorioProduto
            .Setup(r => r.Excluir(produto.Id))
            .Returns(true);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        // repositorioItemPedido.Object
        );

        // Ação
        Result resultado = servicoProduto.Excluir(produto.Id);

        // Asserção
        Assert.IsTrue(resultado.IsSuccess);

        repositorioProduto.Verify(
            r => r.Excluir(produto.Id),
            Times.Once
        );
    }


    [TestMethod]
    public void Excluir_ProdutoNaoEncontrado_RetornaErro()
    {
        // Arranjo
        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto
            .Setup(r => r.SelecionarPorId(It.IsAny<Guid>()))
            .Returns((Produto?)null);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        Guid id = Guid.NewGuid();

        // Ação
        Result resultado = servicoProduto.Excluir(id);

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Produto não encontrado.",
            resultado.Errors.Single().Message
        );

        repositorioProduto.Verify(
            r => r.Excluir(It.IsAny<Guid>()),
            Times.Never
        );
    }


    /*
    =========================================================
    EXCLUSÃO COM ITENS DO PEDIDO
    =========================================================

    Ainda não implementar enquanto o módulo ItemPedido não existir.

    Exemplo da ideia:

    [TestMethod]
    public void Excluir_ComItensPedidoVinculados_NaoExcluiProduto()
    {
        // Mock<IRepositorioItemPedido> repositorioItemPedido = new();

        // ItemPedido itemPedido = ...

        // repositorioItemPedido
        //     .Setup(r => r.Filtrar(It.IsAny<Func<ItemPedido, bool>>()))
        //     .Returns([itemPedido]);

        // ServicoProduto servicoProduto = new(
        //     repositorioProduto.Object,
        //     repositorioItemPedido.Object
        // );

        // Result resultado =
        //     servicoProduto.Excluir(produto.Id);

        // Assert.IsTrue(resultado.IsFailed);

        // repositorioProduto.Verify(
        //     r => r.Excluir(produto.Id),
        //     Times.Never
        // );
    }
    */


    // =========================================================
    // SELEÇÃO
    // =========================================================

    [TestMethod]
    public void SelecionarTodos_RetornaProdutos()
    {
        // Arranjo
        Produto produto1 = new(
            "Cerveja",
            10.50m
        );

        Produto produto2 = new(
            "Refrigerante",
            8.50m
        );

        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto
            .Setup(r => r.SelecionarTodos())
            .Returns([
                produto1,
                produto2
            ]);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        // Ação
        List<ListarProdutosDto> resultado =
            servicoProduto.SelecionarTodos();

        // Asserção
        Assert.IsNotNull(resultado);
        Assert.HasCount(2, resultado);

        Assert.AreEqual(
            produto1.Id,
            resultado[0].Id
        );

        Assert.AreEqual(
            produto1.Nome,
            resultado[0].Nome
        );

        Assert.AreEqual(
            produto1.Preco,
            resultado[0].Preco
        );

        Assert.AreEqual(
            produto2.Id,
            resultado[1].Id
        );

        Assert.AreEqual(
            produto2.Nome,
            resultado[1].Nome
        );

        Assert.AreEqual(
            produto2.Preco,
            resultado[1].Preco
        );
    }


    [TestMethod]
    public void SelecionarPorId_ProdutoExistente_RetornaProduto()
    {
        // Arranjo
        Produto produto = new(
            "Cerveja",
            10.50m
        );

        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto
            .Setup(r => r.SelecionarPorId(produto.Id))
            .Returns(produto);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        // Ação
        Result<DetalhesProdutoDto> resultado =
            servicoProduto.SelecionarPorId(produto.Id);

        // Asserção
        Assert.IsTrue(resultado.IsSuccess);

        Assert.AreEqual(
            produto.Id,
            resultado.Value.Id
        );

        Assert.AreEqual(
            produto.Nome,
            resultado.Value.Nome
        );

        Assert.AreEqual(
            produto.Preco,
            resultado.Value.Preco
        );
    }


    [TestMethod]
    public void SelecionarPorId_ProdutoInexistente_RetornaErro()
    {
        // Arranjo
        Mock<IRepositorioProduto> repositorioProduto = new();

        Guid id = Guid.NewGuid();

        repositorioProduto
            .Setup(r => r.SelecionarPorId(id))
            .Returns((Produto?)null);

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        // Ação
        Result<DetalhesProdutoDto> resultado =
            servicoProduto.SelecionarPorId(id);

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Produto não encontrado.",
            resultado.Errors.Single().Message
        );
    }


    // =========================================================
    // PESQUISA POR NOME
    // =========================================================

    [TestMethod]
    public void PesquisarPorNome_ProdutoExistente_RetornaProduto()
    {
        // Arranjo
        Produto produto = new(
            "Cerveja",
            10.50m
        );

        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto
            .Setup(r => r.Filtrar(
                It.IsAny<Func<Produto, bool>>()
            ))
            .Returns((Func<Produto, bool> filtro) =>
                new List<Produto>
                {
                    produto
                }
                .Where(filtro)
                .ToList()
            );

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        // Ação
        List<ListarProdutosDto> resultado =
            servicoProduto.PesquisarPorNome("Cerveja");

        // Asserção
        Assert.IsNotNull(resultado);
        Assert.HasCount(1, resultado);

        Assert.AreEqual(
            produto.Id,
            resultado[0].Id
        );

        Assert.AreEqual(
            produto.Nome,
            resultado[0].Nome
        );

        Assert.AreEqual(
            produto.Preco,
            resultado[0].Preco
        );
    }


    [TestMethod]
    public void PesquisarPorNome_ProdutoInexistente_RetornaListaVazia()
    {
        // Arranjo
        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto
            .Setup(r => r.Filtrar(
                It.IsAny<Func<Produto, bool>>()
            ))
            .Returns((Func<Produto, bool> filtro) =>
                new List<Produto>()
                    .Where(filtro)
                    .ToList()
            );

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        // Ação
        List<ListarProdutosDto> resultado =
            servicoProduto.PesquisarPorNome("Cerveja");

        // Asserção
        Assert.IsNotNull(resultado);
        Assert.HasCount(0, resultado);
    }


    [TestMethod]
    public void PesquisarPorNome_IgnoraMaiusculasEMinusculas_RetornaProduto()
    {
        // Arranjo
        Produto produto = new(
            "Cerveja",
            10.50m
        );

        Mock<IRepositorioProduto> repositorioProduto = new();

        repositorioProduto
            .Setup(r => r.Filtrar(
                It.IsAny<Func<Produto, bool>>()
            ))
            .Returns((Func<Produto, bool> filtro) =>
                new List<Produto>
                {
                    produto
                }
                .Where(filtro)
                .ToList()
            );

        ServicoProduto servicoProduto = new(
            repositorioProduto.Object
        );

        // Ação
        List<ListarProdutosDto> resultado =
            servicoProduto.PesquisarPorNome("cerVEJa");

        // Asserção
        Assert.IsNotNull(resultado);
        Assert.HasCount(1, resultado);

        Assert.AreEqual(
            produto.Nome,
            resultado[0].Nome
        );
    }
}

