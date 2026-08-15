using FizzWare.NBuilder;
using GerenciamentoDeBar.Dominio.Modulos.ModuloMesa;
using GerenciamentoDeBar.Testes.Integracao.Compartilhado;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GerenciamentoDeBar.Testes.Integraçao.Modulos.ModuloMesa;

[TestClass]
public class RepositorioMesaEmOrmTests : RepositorioBaseEmOrmTests
{
    [TestMethod]
    public void CadastrarESelecionarPorId_CarregaMesa_ComTodosOsCampos()
    {
        // Arranjo
        Mesa mesa = Builder<Mesa>
            .CreateNew()
            .With(m => m.NumeroDaMesa = 1)
            .With(m => m.QuantidadeDeLugares = 4)
            .With(m => m.StatusMesa = StatusMesa.Livre)
            .With(m => m.UserId = Guid.Empty)
            .Build();

        // Ação
        repositorioMesa.Cadastrar(mesa);
        dbContext.ChangeTracker.Clear();

        Mesa? mesaSelecionada =
            repositorioMesa.SelecionarPorId(mesa.Id);

        // Asserção
        Assert.IsNotNull(mesaSelecionada);

        Assert.AreEqual(
            mesa.NumeroDaMesa,
            mesaSelecionada.NumeroDaMesa
        );

        Assert.AreEqual(
            mesa.QuantidadeDeLugares,
            mesaSelecionada.QuantidadeDeLugares
        );

        Assert.AreEqual(
            mesa.StatusMesa,
            mesaSelecionada.StatusMesa
        );
    }

    [TestMethod]
    public void Editar_AtualizaMesa()
    {
        // Arranjo
        Mesa mesa = Builder<Mesa>
            .CreateNew()
            .With(m => m.NumeroDaMesa = 1)
            .With(m => m.QuantidadeDeLugares = 4)
            .With(m => m.StatusMesa = StatusMesa.Livre)
            .With(m => m.UserId = Guid.Empty)
            .Build();

        repositorioMesa.Cadastrar(mesa);
        dbContext.ChangeTracker.Clear();

        Mesa mesaAtualizada = Builder<Mesa>
            .CreateNew()
            .With(m => m.NumeroDaMesa = 2)
            .With(m => m.QuantidadeDeLugares = 2)
            .With(m => m.StatusMesa = StatusMesa.Livre)
            .With(m => m.UserId = Guid.Empty)
            .Build();

        // Ação
        bool conseguiuEditar = repositorioMesa.Editar(
            mesa.Id,
            mesaAtualizada
        );

        dbContext.ChangeTracker.Clear();

        Mesa? mesaSelecionada =
            repositorioMesa.SelecionarPorId(mesa.Id);

        // Asserção
        Assert.IsTrue(conseguiuEditar);

        Assert.IsNotNull(mesaSelecionada);

        Assert.AreEqual(
            mesaAtualizada.NumeroDaMesa,
            mesaSelecionada.NumeroDaMesa
        );

        Assert.AreEqual(
            mesaAtualizada.QuantidadeDeLugares,
            mesaSelecionada.QuantidadeDeLugares
        );

        Assert.AreEqual(
            mesaAtualizada.StatusMesa,
            mesaSelecionada.StatusMesa
        );
    }

    [TestMethod]
    public void Excluir_DeletaMesa()
    {
        // Arranjo
        Mesa mesa = Builder<Mesa>
            .CreateNew()
            .With(m => m.NumeroDaMesa = 1)
            .With(m => m.QuantidadeDeLugares = 4)
            .With(m => m.StatusMesa = StatusMesa.Livre)
            .With(m => m.UserId = Guid.Empty)
            .Build();

        repositorioMesa.Cadastrar(mesa);
        dbContext.ChangeTracker.Clear();

        //Ação
        bool conseguiuExcluir = repositorioMesa.Excluir(mesa.Id);

        dbContext.ChangeTracker.Clear();

        Mesa? mesaSelecionada =
            repositorioMesa.SelecionarPorId(mesa.Id);

        //Asserção
        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(mesaSelecionada);
    }

    [TestMethod]
    public void SelecionarTodos_RetornaMesa()
    {
        // Arranjo
        Mesa mesa = Builder<Mesa>
            .CreateNew()
            .With(m => m.NumeroDaMesa = 1)
            .With(m => m.QuantidadeDeLugares = 4)
            .With(m => m.StatusMesa = StatusMesa.Livre)
            .With(m => m.UserId = Guid.Empty)
            .Build();

        repositorioMesa.Cadastrar(mesa);
        dbContext.ChangeTracker.Clear();

        Mesa mesa2 = Builder<Mesa>
            .CreateNew()
            .With(m => m.NumeroDaMesa = 2)
            .With(m => m.QuantidadeDeLugares = 6)
            .With(m => m.StatusMesa = StatusMesa.Livre)
            .With(m => m.UserId = Guid.Empty)
            .Build();

        repositorioMesa.Cadastrar(mesa2);
        dbContext.ChangeTracker.Clear();

        //Ação
        List<Mesa> mesas = new List<Mesa>([mesa, mesa2]);
        mesas = repositorioMesa.SelecionarTodos();

        //Asserção
        Assert.HasCount(2, mesas);
    }
}
