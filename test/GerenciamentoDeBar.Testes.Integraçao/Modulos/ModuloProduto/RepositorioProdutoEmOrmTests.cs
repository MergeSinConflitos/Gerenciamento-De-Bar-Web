using FizzWare.NBuilder;
using GerenciamentoDeBar.Dominio.Modulos.ModuloProduto;
using GerenciamentoDeBar.Testes.Integracao.Compartilhado;

namespace GerenciamentoDeBar.Testes.Integraçao.Modulos.ModuloProduto
{
    [TestClass]
    public class RepositorioProdutoEmOrmTests : RepositorioBaseEmOrmTests
    {
        [TestMethod]
        public void CadastrarESelecionarPorId_CarregaProduto_ComTodosOsCampos()
        {
            // Arranjo
            Produto produto = Builder<Produto>
                .CreateNew()
                .With(p => p.Nome = "Cerveja")
                .With(p => p.Preco = 10.50m)
                .With(p => p.UserId = Guid.Empty)
                .Build();

            // Ação
            repositorioProduto.Cadastrar(produto);
            dbContext.ChangeTracker.Clear();

            Produto? produtoSelecionado =
                repositorioProduto.SelecionarPorId(produto.Id);

            // Asserção
            Assert.IsNotNull(produtoSelecionado);

            Assert.AreEqual(
                produto.Nome,
                produtoSelecionado.Nome
            );

            Assert.AreEqual(
                produto.Preco,
                produtoSelecionado.Preco
            );
        }


        [TestMethod]
        public void Editar_AtualizaProduto()
        {
            // Arranjo
            Produto produto = Builder<Produto>
                .CreateNew()
                .With(p => p.Nome = "Cerveja")
                .With(p => p.Preco = 10.50m)
                .With(p => p.UserId = Guid.Empty)
                .Build();

            repositorioProduto.Cadastrar(produto);
            dbContext.ChangeTracker.Clear();

            Produto produtoAtualizado = Builder<Produto>
                .CreateNew()
                .With(p => p.Nome = "Refrigerante")
                .With(p => p.Preco = 7.50m)
                .With(p => p.UserId = Guid.Empty)
                .Build();

            // Ação
            bool conseguiuEditar = repositorioProduto.Editar(
                produto.Id,
                produtoAtualizado
            );

            dbContext.ChangeTracker.Clear();

            Produto? produtoSelecionado =
                repositorioProduto.SelecionarPorId(produto.Id);

            // Asserção
            Assert.IsTrue(conseguiuEditar);

            Assert.IsNotNull(produtoSelecionado);

            Assert.AreEqual(
                produtoAtualizado.Nome,
                produtoSelecionado.Nome
            );

            Assert.AreEqual(
                produtoAtualizado.Preco,
                produtoSelecionado.Preco
            );
        }


        [TestMethod]
        public void Excluir_DeletaProduto()
        {
            // Arranjo
            Produto produto = Builder<Produto>
                .CreateNew()
                .With(p => p.Nome = "Cerveja")
                .With(p => p.Preco = 10.50m)
                .With(p => p.UserId = Guid.Empty)
                .Build();

            repositorioProduto.Cadastrar(produto);
            dbContext.ChangeTracker.Clear();

            // Ação
            bool conseguiuExcluir =
                repositorioProduto.Excluir(produto.Id);

            dbContext.ChangeTracker.Clear();

            Produto? produtoSelecionado =
                repositorioProduto.SelecionarPorId(produto.Id);

            // Asserção
            Assert.IsTrue(conseguiuExcluir);
            Assert.IsNull(produtoSelecionado);
        }


        [TestMethod]
        public void SelecionarTodos_RetornaProdutos()
        {
            // Arranjo
            Produto produto = Builder<Produto>
                .CreateNew()
                .With(p => p.Nome = "Cerveja")
                .With(p => p.Preco = 10.50m)
                .With(p => p.UserId = Guid.Empty)
                .Build();

            repositorioProduto.Cadastrar(produto);
            dbContext.ChangeTracker.Clear();

            Produto produto2 = Builder<Produto>
                .CreateNew()
                .With(p => p.Nome = "Refrigerante")
                .With(p => p.Preco = 7.50m)
                .With(p => p.UserId = Guid.Empty)
                .Build();

            repositorioProduto.Cadastrar(produto2);
            dbContext.ChangeTracker.Clear();

            // Ação
            List<Produto> produtos =
                new List<Produto>([produto, produto2]);

            produtos = repositorioProduto.SelecionarTodos();

            // Asserção
            Assert.HasCount(2, produtos);
        }
    }
}