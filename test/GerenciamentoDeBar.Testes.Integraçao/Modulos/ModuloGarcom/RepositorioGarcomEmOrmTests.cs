using FizzWare.NBuilder;
using GerenciamentoDeBar.Dominio.Modulos.ModuloGarcom;
using GerenciamentoDeBar.Testes.Integracao.Compartilhado;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GerenciamentoDeBar.Testes.Integraçao.Modulos.ModuloGarcom;

[TestClass]
public class RepositorioGarcomEmOrmTests : RepositorioBaseEmOrmTests
{
    [TestMethod]
    public void CadastrarESelecionarPorId_CarregaGarcom_ComTodosOsCampos()
    {
        // Arranjo
        Garcom garcom = Builder<Garcom>
            .CreateNew()
            .With(g => g.Nome = "João")
            .With(g => g.Telefone = "(49) 99999-9999")
            .With(g => g.Cpf = "123.456.789-09")
            .With(g => g.UserId = Guid.Empty)
            .Build();

        // Ação
        repositorioGarcom.Cadastrar(garcom);
        dbContext.ChangeTracker.Clear();

        Garcom? garcomSelecionado =
            repositorioGarcom.SelecionarPorId(garcom.Id);

        // Asserção
        Assert.IsNotNull(garcomSelecionado);

        Assert.AreEqual(
            garcom.Nome,
            garcomSelecionado.Nome
        );

        Assert.AreEqual(
            garcom.Telefone,
            garcomSelecionado.Telefone
        );

        Assert.AreEqual(
            garcom.Cpf,
            garcomSelecionado.Cpf
        );
    }


    [TestMethod]
    public void Editar_AtualizaGarcom()
    {
        // Arranjo
        Garcom garcom = Builder<Garcom>
            .CreateNew()
            .With(g => g.Nome = "João")
            .With(g => g.Telefone = "(49) 99999-9999")
            .With(g => g.Cpf = "123.456.789-09")
            .With(g => g.UserId = Guid.Empty)
            .Build();

        repositorioGarcom.Cadastrar(garcom);
        dbContext.ChangeTracker.Clear();

        Garcom garcomAtualizado = Builder<Garcom>
            .CreateNew()
            .With(g => g.Nome = "Pedro")
            .With(g => g.Telefone = "(49) 98888-8888")
            .With(g => g.Cpf = "123.456.789-09")
            .With(g => g.UserId = Guid.Empty)
            .Build();

        // Ação
        bool conseguiuEditar = repositorioGarcom.Editar(
            garcom.Id,
            garcomAtualizado
        );

        dbContext.ChangeTracker.Clear();

        Garcom? garcomSelecionado =
            repositorioGarcom.SelecionarPorId(garcom.Id);

        // Asserção
        Assert.IsTrue(conseguiuEditar);

        Assert.IsNotNull(garcomSelecionado);

        Assert.AreEqual(
            garcomAtualizado.Nome,
            garcomSelecionado.Nome
        );

        Assert.AreEqual(
            garcomAtualizado.Telefone,
            garcomSelecionado.Telefone
        );

        Assert.AreEqual(
            garcomAtualizado.Cpf,
            garcomSelecionado.Cpf
        );
    }


    [TestMethod]
    public void Excluir_DeletaGarcom()
    {
        // Arranjo
        Garcom garcom = Builder<Garcom>
            .CreateNew()
            .With(g => g.Nome = "João")
            .With(g => g.Telefone = "(49) 99999-9999")
            .With(g => g.Cpf = "123.456.789-09")
            .With(g => g.UserId = Guid.Empty)
            .Build();

        repositorioGarcom.Cadastrar(garcom);
        dbContext.ChangeTracker.Clear();

        // Ação
        bool conseguiuExcluir =
            repositorioGarcom.Excluir(garcom.Id);

        dbContext.ChangeTracker.Clear();

        Garcom? garcomSelecionado =
            repositorioGarcom.SelecionarPorId(garcom.Id);

        // Asserção
        Assert.IsTrue(conseguiuExcluir);

        Assert.IsNull(garcomSelecionado);
    }


    [TestMethod]
    public void SelecionarTodos_RetornaGarcons()
    {
        // Arranjo
        Garcom garcom = Builder<Garcom>
            .CreateNew()
            .With(g => g.Nome = "João")
            .With(g => g.Telefone = "(49) 99999-9999")
            .With(g => g.Cpf = "123.456.789-09")
            .With(g => g.UserId = Guid.Empty)
            .Build();

        repositorioGarcom.Cadastrar(garcom);
        dbContext.ChangeTracker.Clear();

        Garcom garcom2 = Builder<Garcom>
            .CreateNew()
            .With(g => g.Nome = "Pedro")
            .With(g => g.Telefone = "(49) 98888-8888")
            .With(g => g.Cpf = "987.654.321-00")
            .With(g => g.UserId = Guid.Empty)
            .Build();

        repositorioGarcom.Cadastrar(garcom2);
        dbContext.ChangeTracker.Clear();

        // Ação
        List<Garcom> garcons =
            repositorioGarcom.SelecionarTodos();

        // Asserção
        Assert.HasCount(2, garcons);
    }
}
