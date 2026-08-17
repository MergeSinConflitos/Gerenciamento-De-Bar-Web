using FluentResults;
using GerenciamentoDeBar.Aplicacao.Modulos.ModuloMesa;
using GerenciamentoDeBar.Dominio.Modulos.ModuloMesa;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using static GerenciamentoDeBar.Aplicacao.Modulos.ModuloMesa.MesaDtos;

namespace GerenciamentoDeBar.Testes.Unidade.ModuloMesa;

[TestClass]
public class ServicoMesaTests
{
    [TestMethod]
    public void Cadastrar_ComDadosValidos_PersisteMesa()
    {
        //Arranjo
        Mock<IRepositorioMesa> repositorioMesa = new Mock<IRepositorioMesa>();
        // Mock<IRepositorioConta> repositorioConta = new Mock<IRepositorioConta>();

        repositorioMesa.Setup(r => r.SelecionarTodos()).Returns([]);

        Mesa? mesaCadastrada = null;

        repositorioMesa.Setup(r => r.Cadastrar(It.IsAny<Mesa>()))
        .Callback<Mesa>(mesa => mesaCadastrada = mesa);

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object
        // repositorioConta.Object
        );

        //Ação
        Result resultado = servicoMesa.Cadastrar(new MesaDtos.CadastrarMesaDto(1, 4));

        //Asserção
        Assert.IsTrue(resultado.IsSuccess);
        Assert.IsNotNull(mesaCadastrada);
        Assert.AreEqual(1, mesaCadastrada.NumeroDaMesa);
        Assert.AreEqual(4, mesaCadastrada.QuantidadeDeLugares);


        repositorioMesa.Verify(r => r.Cadastrar(It.IsAny<Mesa>()), Times.Once);
    }

    [TestMethod]
    public void Cadastrar_ComNumeroZero_NaoPersisteMesa()
    {
        // Arranjo
        Mock<IRepositorioMesa> repositorioMesa =
            new Mock<IRepositorioMesa>();

        //Mock<IRepositorioConta> repositorioConta =
        // new Mock<IRepositorioConta>();

        repositorioMesa
        .Setup(r => r.SelecionarTodos())
        .Returns([]);

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object
        //  repositorioConta.Object
        );

        // Ação
        Result resultado =
            servicoMesa.Cadastrar(
                new CadastrarMesaDto(0, 4)
            );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Informe um número positivo",
            resultado.Errors.Single().Message
        );

        repositorioMesa.Verify(
            r => r.Cadastrar(It.IsAny<Mesa>()),
            Times.Never
        );
    }


    [TestMethod]
    public void Cadastrar_ComNumeroNegativo_NaoPersisteMesa()
    {
        // Arranjo
        Mock<IRepositorioMesa> repositorioMesa =
            new Mock<IRepositorioMesa>();

        //Mock<IRepositorioConta> repositorioConta =
        // new Mock<IRepositorioConta>();

        repositorioMesa
        .Setup(r => r.SelecionarTodos())
        .Returns([]);

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object
        //  repositorioConta.Object
        );

        // Ação
        Result resultado =
            servicoMesa.Cadastrar(
                new CadastrarMesaDto(-1, 4)
            );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Informe um número positivo",
            resultado.Errors.Single().Message
        );

        repositorioMesa.Verify(
            r => r.Cadastrar(It.IsAny<Mesa>()),
            Times.Never
        );
    }


    [TestMethod]
    public void Cadastrar_ComQuantidadeDeLugarZero_NaoPersisteMesa()
    {
        // Arranjo
        Mock<IRepositorioMesa> repositorioMesa =
            new Mock<IRepositorioMesa>();

        //Mock<IRepositorioConta> repositorioConta =
        // new Mock<IRepositorioConta>();

        repositorioMesa
        .Setup(r => r.SelecionarTodos())
        .Returns([]);

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object
        //  repositorioConta.Object
        );

        // Ação
        Result resultado =
            servicoMesa.Cadastrar(
                new CadastrarMesaDto(1, 0)
            );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Informe um número positivo",
            resultado.Errors.Single().Message
        );

        repositorioMesa.Verify(
            r => r.Cadastrar(It.IsAny<Mesa>()),
            Times.Never
        );
    }

    [TestMethod]
    public void Cadastrar_ComQuantidadeDeLugarNegativa_NaoPersisteMesa()
    {
        // Arranjo
        Mock<IRepositorioMesa> repositorioMesa =
            new Mock<IRepositorioMesa>();

        //Mock<IRepositorioConta> repositorioConta =
        // new Mock<IRepositorioConta>();

        repositorioMesa
        .Setup(r => r.SelecionarTodos())
        .Returns([]);

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object
        //  repositorioConta.Object
        );

        // Ação
        Result resultado =
            servicoMesa.Cadastrar(
                new CadastrarMesaDto(1, -1)
            );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Informe um número positivo",
            resultado.Errors.Single().Message
        );

        repositorioMesa.Verify(
            r => r.Cadastrar(It.IsAny<Mesa>()),
            Times.Never
        );
    }

    [TestMethod]
    public void Cadastrar_ComQuantidadeDeLugarAcimaDoLimite_NaoPersisteMesa()
    {
        // Arranjo
        Mock<IRepositorioMesa> repositorioMesa =
            new Mock<IRepositorioMesa>();

        //Mock<IRepositorioConta> repositorioConta =
        // new Mock<IRepositorioConta>();

        repositorioMesa
        .Setup(r => r.SelecionarTodos())
        .Returns([]);

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object
        //  repositorioConta.Object
        );

        // Ação
        Result resultado =
            servicoMesa.Cadastrar(
                new CadastrarMesaDto(1, 11)
            );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "A quantidade máxima é de 10 lugares",
            resultado.Errors.Single().Message
        );

        repositorioMesa.Verify(
            r => r.Cadastrar(It.IsAny<Mesa>()),
            Times.Never
        );
    }

    [TestMethod]
    public void Cadastrar_ComNumeroJaExistente_NaoPersisteMesa()
    {
        // Arranjo
        Mesa mesaExistente = new Mesa(
            1,
            4
        );

        Mock<IRepositorioMesa> repositorioMesa =
            new Mock<IRepositorioMesa>();

        //Mock<IRepositorioConta> repositorioConta =
        // new Mock<IRepositorioConta>();

        repositorioMesa
            .Setup(r => r.SelecionarTodos())
            .Returns([mesaExistente]);

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object
        // repositorioConta.Object
        );

        // Ação
        Result resultado =
            servicoMesa.Cadastrar(
                new CadastrarMesaDto(1, 4)
            );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Já existe",
            resultado.Errors.Single().Message
        );

        repositorioMesa.Verify(
            r => r.Cadastrar(It.IsAny<Mesa>()),
            Times.Never
        );

    }

    [TestMethod]
    public void Editar_ComDadosValidos_PersisteMesa()
    {
        // Arranjo
        Mesa mesaExistente = new Mesa(1, 4);

        Mesa? mesaAtualizada = null;

        Mock<IRepositorioMesa> repositorioMesa = new Mock<IRepositorioMesa>();

        // Mock<IRepositorioConta> repositorioConta =
        //     new Mock<IRepositorioConta>();

        repositorioMesa
            .Setup(r => r.SelecionarTodos())
            .Returns([mesaExistente]);

        repositorioMesa
            .Setup(r => r.Editar(
                mesaExistente.Id,
                It.IsAny<Mesa>()
            ))
            .Callback<Guid, Mesa>((id, mesa) =>
            {
                mesaAtualizada = mesa;
            })
            .Returns(true);

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object
        // repositorioConta.Object
        );

        // Ação
        Result resultado = servicoMesa.Editar(
            new EditarMesaDto(
                mesaExistente.Id,
                2,
                6
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsSuccess);
        Assert.IsNotNull(mesaAtualizada);

        Assert.AreEqual(2, mesaAtualizada.NumeroDaMesa);
        Assert.AreEqual(6, mesaAtualizada.QuantidadeDeLugares);

        repositorioMesa.Verify(
            r => r.Editar(
                mesaExistente.Id,
                It.IsAny<Mesa>()
            ),
            Times.Once
        );
    }

    [TestMethod]
    public void Editar_ComNumeroJaExistente_NaoPersisteMesa()
    {
        // Arranjo
        Mesa mesaExistente = new Mesa(1, 4);
        Mesa outraMesa = new Mesa(2, 4);

        Mock<IRepositorioMesa> repositorioMesa =
            new Mock<IRepositorioMesa>();

        // Mock<IRepositorioConta> repositorioConta =
        //     new Mock<IRepositorioConta>();

        repositorioMesa
            .Setup(r => r.SelecionarTodos())
            .Returns([mesaExistente, outraMesa]);

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object
        // repositorioConta.Object
        );

        // Ação
        Result resultado = servicoMesa.Editar(
            new EditarMesaDto(
                mesaExistente.Id,
                2,
                4
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Já existe",
            resultado.Errors.Single().Message
        );

        repositorioMesa.Verify(
            r => r.Editar(
                mesaExistente.Id,
                It.IsAny<Mesa>()
            ),
            Times.Never
        );
    }

    [TestMethod]
    public void Editar_ComNumeroZero_NaoPersisteMesa()
    {
        // Arranjo
        Mesa mesaExistente = new Mesa(1, 4);

        Mock<IRepositorioMesa> repositorioMesa =
            new Mock<IRepositorioMesa>();

        // Mock<IRepositorioConta> repositorioConta =
        //     new Mock<IRepositorioConta>();

        repositorioMesa
            .Setup(r => r.SelecionarTodos())
            .Returns([mesaExistente]);

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object
        // repositorioConta.Object
        );

        // Ação
        Result resultado = servicoMesa.Editar(
            new EditarMesaDto(
                mesaExistente.Id,
                0,
                4
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Informe um número positivo",
            resultado.Errors.Single().Message
        );

        repositorioMesa.Verify(
            r => r.Editar(
                mesaExistente.Id,
                It.IsAny<Mesa>()
            ),
            Times.Never
        );
    }

    [TestMethod]
    public void Editar_ComNumeroNegativo_NaoPersisteMesa()
    {
        // Arranjo
        Mesa mesaExistente = new Mesa(1, 4);

        Mock<IRepositorioMesa> repositorioMesa =
            new Mock<IRepositorioMesa>();

        // Mock<IRepositorioConta> repositorioConta =
        //     new Mock<IRepositorioConta>();

        repositorioMesa
            .Setup(r => r.SelecionarTodos())
            .Returns([mesaExistente]);

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object
        // repositorioConta.Object
        );

        // Ação
        Result resultado = servicoMesa.Editar(
            new EditarMesaDto(
                mesaExistente.Id,
                -1,
                4
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Informe um número positivo",
            resultado.Errors.Single().Message
        );

        repositorioMesa.Verify(
            r => r.Editar(
                mesaExistente.Id,
                It.IsAny<Mesa>()
            ),
            Times.Never
        );
    }

    [TestMethod]
    public void Editar_ComQuantidadeDeLugarZero_NaoPersisteMesa()
    {
        // Arranjo
        Mesa mesaExistente = new Mesa(1, 4);

        Mock<IRepositorioMesa> repositorioMesa =
            new Mock<IRepositorioMesa>();

        // Mock<IRepositorioConta> repositorioConta =
        //     new Mock<IRepositorioConta>();

        repositorioMesa
            .Setup(r => r.SelecionarTodos())
            .Returns([mesaExistente]);

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object
        // repositorioConta.Object
        );

        // Ação
        Result resultado = servicoMesa.Editar(
            new EditarMesaDto(
                mesaExistente.Id,
                1,
                0
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Informe um número positivo",
            resultado.Errors.Single().Message
        );

        repositorioMesa.Verify(
            r => r.Editar(
                mesaExistente.Id,
                It.IsAny<Mesa>()
            ),
            Times.Never
        );
    }

    [TestMethod]
    public void Editar_ComQuantidadeDeLugarNegativa_NaoPersisteMesa()
    {
        // Arranjo
        Mesa mesaExistente = new Mesa(1, 4);

        Mock<IRepositorioMesa> repositorioMesa =
            new Mock<IRepositorioMesa>();

        // Mock<IRepositorioConta> repositorioConta =
        //     new Mock<IRepositorioConta>();

        repositorioMesa
            .Setup(r => r.SelecionarTodos())
            .Returns([mesaExistente]);

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object
        // repositorioConta.Object
        );

        // Ação
        Result resultado = servicoMesa.Editar(
            new EditarMesaDto(
                mesaExistente.Id,
                1,
                -1
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Informe um número positivo",
            resultado.Errors.Single().Message
        );

        repositorioMesa.Verify(
            r => r.Editar(
                mesaExistente.Id,
                It.IsAny<Mesa>()
            ),
            Times.Never
        );
    }

    [TestMethod]
    public void Editar_ComQuantidadeDeLugarAcimaDoLimite_NaoPersisteMesa()
    {
        // Arranjo
        Mesa mesaExistente = new Mesa(1, 4);

        Mock<IRepositorioMesa> repositorioMesa =
            new Mock<IRepositorioMesa>();

        // Mock<IRepositorioConta> repositorioConta =
        //     new Mock<IRepositorioConta>();

        repositorioMesa
            .Setup(r => r.SelecionarTodos())
            .Returns([mesaExistente]);

        ServicoMesa servicoMesa = new ServicoMesa(
            repositorioMesa.Object
        // repositorioConta.Object
        );

        // Ação
        Result resultado = servicoMesa.Editar(
            new EditarMesaDto(
                mesaExistente.Id,
                1,
                11
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "A quantidade máxima é de 10 lugares",
            resultado.Errors.Single().Message
        );

        repositorioMesa.Verify(
            r => r.Editar(
                mesaExistente.Id,
                It.IsAny<Mesa>()
            ),
            Times.Never
        );
    }

    [TestMethod]
    public void Excluir_SemContasVinculadas_ExcluiMesa()
    {
        // Arranjo
        Mesa mesa = new Mesa(
            1,
            4
        );

        Mock<IRepositorioMesa> repositorioMesa = new();

        // Mock<IRepositorioConta> repositorioConta = new();

        repositorioMesa
            .Setup(r => r.SelecionarPorId(mesa.Id))
            .Returns(mesa);

        // repositorioConta
        //     .Setup(r => r.SelecionarTodos())
        //     .Returns([]);

        repositorioMesa
            .Setup(r => r.Excluir(mesa.Id))
            .Returns(true);

        ServicoMesa servicoMesa = new(
            repositorioMesa.Object
        // repositorioConta.Object
        );

        // Ação
        Result resultado = servicoMesa.Excluir(mesa.Id);

        // Asserção
        Assert.IsTrue(resultado.IsSuccess);

        repositorioMesa.Verify(
            r => r.Excluir(mesa.Id),
            Times.Once
        );
    }

    [TestMethod]
    public void PesquisarPorNumero_MesaExistente_RetornaMesa()
    {
        // Arranjo
        Mesa mesa1 = new Mesa(
            1,
            4
        );

        Mesa mesa2 = new Mesa(
            2,
            6
        );

        Mock<IRepositorioMesa> repositorioMesa = new();

        repositorioMesa
            .Setup(r => r.Filtrar(It.IsAny<Func<Mesa, bool>>()))
           .Returns((Func<Mesa, bool> filtro) =>
         new List<Mesa> { mesa1, mesa2 }
        .Where(filtro)
        .ToList());


        ServicoMesa servicoMesa = new(
            repositorioMesa.Object
        );

        // Ação
        Result<DetalhesMesaDto> resultado =
            servicoMesa.PesquisarPorNumero(1);

        // Asserção
        Assert.IsTrue(resultado.IsSuccess);

        Assert.IsNotNull(resultado.Value);

        Assert.AreEqual(mesa1.Id, resultado.Value.Id);
        Assert.AreEqual(mesa1.NumeroDaMesa, resultado.Value.NumeroDaMesa);
        Assert.AreEqual(mesa1.QuantidadeDeLugares, resultado.Value.QuantidadeDeLugares);
        Assert.AreEqual(mesa1.StatusMesa, resultado.Value.StatusMesa);
    }

    [TestMethod]
    public void PesquisarPorStatus_MesasComStatusExistente_RetornaMesas()
    {
        // Arranjo
        Mesa mesa1 = new Mesa(
            1,
            4
        );

        Mesa mesa2 = new Mesa(
            2,
            6
        );

        Mesa mesa3 = new Mesa(
            3,
            4
        );

        mesa1.StatusMesa = StatusMesa.Livre;
        mesa2.StatusMesa = StatusMesa.Ocupada;
        mesa3.StatusMesa = StatusMesa.Livre;

        Mock<IRepositorioMesa> repositorioMesa = new();

        repositorioMesa
            .Setup(r => r.Filtrar(It.IsAny<Func<Mesa, bool>>()))
            .Returns((Func<Mesa, bool> filtro) =>
                new List<Mesa> { mesa1, mesa2, mesa3 }
                    .Where(filtro)
                    .ToList());

        ServicoMesa servicoMesa = new(
            repositorioMesa.Object
        );

        // Ação
        List<ListarMesasDto> resultado =
            servicoMesa.PesquisarPorStatus(StatusMesa.Livre);

        // Asserção
        Assert.IsNotNull(resultado);

        Assert.HasCount(2, resultado);

        Assert.AreEqual(mesa1.Id, resultado[0].Id);
        Assert.AreEqual(mesa1.NumeroDaMesa, resultado[0].NumeroDaMesa);

        Assert.AreEqual(mesa3.Id, resultado[1].Id);
        Assert.AreEqual(mesa3.NumeroDaMesa, resultado[1].NumeroDaMesa);
    }
    /*
    [TestMethod]
    public void Excluir_ComContasVinculadas_NaoExcluiMesa()
    {
        // Arranjo
        Mesa mesa = new Mesa(
            1,
            4
        );

        Mock<IRepositorioMesa> repositorioMesa = new();

        // Mock<IRepositorioConta> repositorioConta = new();

        repositorioMesa
            .Setup(r => r.SelecionarPorId(mesa.Id))
            .Returns(mesa);

        // Conta conta = new Conta(...);

        // repositorioConta
        //     .Setup(r => r.SelecionarTodos())
        //     .Returns([conta]);

        // repositorioMesa
        //     .Setup(r => r.Excluir(mesa.Id))
        //     .Returns(false);

        ServicoMesa servicoMesa = new(
            repositorioMesa.Object
            // repositorioConta.Object
        );

        // Ação
        Result resultado = servicoMesa.Excluir(mesa.Id);

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        // repositorioMesa.Verify(
        //     r => r.Excluir(mesa.Id),
        //     Times.Never
        // );
    }
    */


}
