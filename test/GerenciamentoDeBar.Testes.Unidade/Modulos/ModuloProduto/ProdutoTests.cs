
using GerenciamentoDeBar.Dominio.Modulos.ModuloProduto;

namespace GerenciamentoDeBar.Testes.Unidade.Modulos.ModuloProduto;

[TestClass]
public class ProdutoTests
{
    // =========================================================
    // CADASTRO
    // =========================================================

    [TestMethod]
    public void Deve_CriarProduto_ComDadosValidos()
    {
        // Arrange
        Produto produto = new Produto(
            "Cerveja",
            10.50m
        );

        // Act
        List<string> erros = produto.Validar();

        // Assert
        Assert.IsEmpty(erros);
    }


    // =========================================================
    // VALIDAÇÃO - NOME
    // =========================================================

    [TestMethod]
    public void NaoDeve_CriarProduto_ComNomeVazio()
    {
        // Arrange
        Produto produto = new Produto(
            "",
            10.50m
        );

        // Act
        List<string> erros = produto.Validar();

        // Assert
        CollectionAssert.Contains(
            erros,
            "O campo \"Nome\" deve ser preenchido"
        );
    }


    [TestMethod]
    public void NaoDeve_CriarProduto_ComNomeMuitoCurto()
    {
        // Arrange
        Produto produto = new Produto(
            "A",
            10.50m
        );

        // Act
        List<string> erros = produto.Validar();

        // Assert
        CollectionAssert.Contains(
            erros,
            "O nome deve ter entre 2 e 100 caracteres"
        );
    }


    [TestMethod]
    public void NaoDeve_CriarProduto_ComNomeMuitoLongo()
    {
        // Arrange
        string nome = new string('A', 101);

        Produto produto = new Produto(
            nome,
            10.50m
        );

        // Act
        List<string> erros = produto.Validar();

        // Assert
        CollectionAssert.Contains(
            erros,
            "O nome deve ter entre 2 e 100 caracteres"
        );
    }


    // =========================================================
    // VALIDAÇÃO - PREÇO
    // =========================================================

    [TestMethod]
    public void NaoDeve_CriarProduto_ComPrecoZero()
    {
        // Arrange
        Produto produto = new Produto(
            "Cerveja",
            0
        );

        // Act
        List<string> erros = produto.Validar();

        // Assert
        CollectionAssert.Contains(
            erros,
            "Informe um valor positivo"
        );
    }


    [TestMethod]
    public void NaoDeve_CriarProduto_ComPrecoNegativo()
    {
        // Arrange
        Produto produto = new Produto(
            "Cerveja",
            -10.50m
        );

        // Act
        List<string> erros = produto.Validar();

        // Assert
        CollectionAssert.Contains(
            erros,
            "Informe um valor positivo"
        );
    }


    [TestMethod]
    public void Deve_CriarProduto_ComPrecoPositivo()
    {
        // Arrange
        Produto produto = new Produto(
            "Cerveja",
            10.50m
        );

        // Act
        List<string> erros = produto.Validar();

        // Assert
        Assert.AreEqual(0, erros.Count);
    }


    // =========================================================
    // EDIÇÃO
    // =========================================================

    [TestMethod]
    public void Deve_EditarProduto_ComDadosValidos()
    {
        // Arrange
        Produto produto = new Produto(
            "Cerveja",
            10.50m
        );

        Produto produtoAtualizado = new Produto(
            "Refrigerante",
            7.50m
        );

        // Act
        produto.Atualizar(produtoAtualizado);

        // Assert
        Assert.AreEqual(
            "Refrigerante",
            produto.Nome
        );

        Assert.AreEqual(
            7.50m,
            produto.Preco
        );
    }
}