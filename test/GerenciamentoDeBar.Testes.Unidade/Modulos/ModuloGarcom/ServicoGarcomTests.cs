using FluentResults;
using GerenciamentoDeBar.Aplicacao.Modulos.ModuloGarcom;
using GerenciamentoDeBar.Dominio.Modulos.ModuloGarcom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using static GerenciamentoDeBar.Aplicacao.Modulos.ModuloGarcom.GarcomDtos;

namespace GerenciamentoDeBar.Testes.Unidade.Modulos.ModuloGarcom;

[TestClass]
public class ServicoGarcomTests
{
    [TestMethod]
    public void Cadastrar_ComDadosValidos_PersisteGarcom()
    {
        // Arranjo
        Mock<IRepositorioGarcom> repositorioGarcom = new();

        repositorioGarcom
            .Setup(r => r.SelecionarTodos())
            .Returns([]);

        Garcom? garcomCadastrado = null;

        repositorioGarcom
            .Setup(r => r.Cadastrar(It.IsAny<Garcom>()))
            .Callback<Garcom>(garcom =>
            {
                garcomCadastrado = garcom;
            });

        ServicoGarcom servicoGarcom = new(
            repositorioGarcom.Object
        // repositorioConta.Object
        );

        // Ação
        Result resultado = servicoGarcom.Cadastrar(
            new GarcomDtos.CadastrarGarcomDto(
                "João",
                "(49) 99999-9999",
                "123.456.789-09"
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsSuccess);

        Assert.IsNotNull(garcomCadastrado);

        Assert.AreEqual(
            "João",
            garcomCadastrado.Nome
        );

        Assert.AreEqual(
            "(49) 99999-9999",
            garcomCadastrado.Telefone
        );

        Assert.AreEqual(
            "123.456.789-09",
            garcomCadastrado.Cpf
        );

        repositorioGarcom.Verify(
            r => r.Cadastrar(It.IsAny<Garcom>()),
            Times.Once
        );
    }


    [TestMethod]
    public void Cadastrar_ComNomeVazio_NaoPersisteGarcom()
    {
        // Arranjo
        Mock<IRepositorioGarcom> repositorioGarcom = new();

        repositorioGarcom
            .Setup(r => r.SelecionarTodos())
            .Returns([]);

        ServicoGarcom servicoGarcom = new(
            repositorioGarcom.Object
        );

        // Ação
        Result resultado = servicoGarcom.Cadastrar(
            new GarcomDtos.CadastrarGarcomDto(
                "",
                "(49) 99999-9999",
                "123.456.789-09"
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "O campo \"Nome\" deve ser preenchido",
            resultado.Errors.Single().Message
        );

        repositorioGarcom.Verify(
            r => r.Cadastrar(It.IsAny<Garcom>()),
            Times.Never
        );
    }


    [TestMethod]
    public void Cadastrar_ComNomeMuitoCurto_NaoPersisteGarcom()
    {
        // Arranjo
        Mock<IRepositorioGarcom> repositorioGarcom = new();

        repositorioGarcom
            .Setup(r => r.SelecionarTodos())
            .Returns([]);

        ServicoGarcom servicoGarcom = new(
            repositorioGarcom.Object
        );

        // Ação
        Result resultado = servicoGarcom.Cadastrar(
            new GarcomDtos.CadastrarGarcomDto(
                "A",
                "(49) 99999-9999",
                "123.456.789-09"
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "O nome deve ter entre 2 e 100 caracteres",
            resultado.Errors.Single().Message
        );

        repositorioGarcom.Verify(
            r => r.Cadastrar(It.IsAny<Garcom>()),
            Times.Never
        );
    }


    [TestMethod]
    public void Cadastrar_ComNomeMuitoLongo_NaoPersisteGarcom()
    {
        // Arranjo
        Mock<IRepositorioGarcom> repositorioGarcom = new();

        repositorioGarcom
            .Setup(r => r.SelecionarTodos())
            .Returns([]);

        ServicoGarcom servicoGarcom = new(
            repositorioGarcom.Object
        );

        string nome = new string('A', 101);

        // Ação
        Result resultado = servicoGarcom.Cadastrar(
            new GarcomDtos.CadastrarGarcomDto(
                nome,
                "(49) 99999-9999",
                "123.456.789-09"
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "O nome deve ter entre 2 e 100 caracteres",
            resultado.Errors.Single().Message
        );

        repositorioGarcom.Verify(
            r => r.Cadastrar(It.IsAny<Garcom>()),
            Times.Never
        );
    }


    [TestMethod]
    public void Cadastrar_ComTelefoneVazio_NaoPersisteGarcom()
    {
        // Arranjo
        Mock<IRepositorioGarcom> repositorioGarcom = new();

        repositorioGarcom
            .Setup(r => r.SelecionarTodos())
            .Returns([]);

        ServicoGarcom servicoGarcom = new(
            repositorioGarcom.Object
        );

        // Ação
        Result resultado = servicoGarcom.Cadastrar(
            new GarcomDtos.CadastrarGarcomDto(
                "João",
                "",
                "123.456.789-09"
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "O campo \"Telefone\" deve ser preenchido",
            resultado.Errors.Single().Message
        );

        repositorioGarcom.Verify(
            r => r.Cadastrar(It.IsAny<Garcom>()),
            Times.Never
        );
    }


    [TestMethod]
    public void Cadastrar_ComCpfVazio_NaoPersisteGarcom()
    {
        // Arranjo
        Mock<IRepositorioGarcom> repositorioGarcom = new();

        repositorioGarcom
            .Setup(r => r.SelecionarTodos())
            .Returns([]);

        ServicoGarcom servicoGarcom = new(
            repositorioGarcom.Object
        );

        // Ação
        Result resultado = servicoGarcom.Cadastrar(
            new GarcomDtos.CadastrarGarcomDto(
                "João",
                "(49) 99999-9999",
                ""
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "O campo \"CPF\" deve ser preenchido",
            resultado.Errors.Single().Message
        );

        repositorioGarcom.Verify(
            r => r.Cadastrar(It.IsAny<Garcom>()),
            Times.Never
        );
    }


    [TestMethod]
    public void Cadastrar_ComTelefoneJaExistente_NaoPersisteGarcom()
    {
        // Arranjo
        Garcom garcomExistente = new Garcom(
            "João",
            "(49) 99999-9999",
            "123.456.789-09"
        );

        Mock<IRepositorioGarcom> repositorioGarcom = new();

        repositorioGarcom
            .Setup(r => r.SelecionarTodos())
            .Returns([garcomExistente]);

        ServicoGarcom servicoGarcom = new(
            repositorioGarcom.Object
        );

        // Ação
        Result resultado = servicoGarcom.Cadastrar(
            new GarcomDtos.CadastrarGarcomDto(
                "Pedro",
                "(49) 99999-9999",
                "987.654.321-00"
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Já existe",
            resultado.Errors.Single().Message
        );

        repositorioGarcom.Verify(
            r => r.Cadastrar(It.IsAny<Garcom>()),
            Times.Never
        );
    }


    [TestMethod]
    public void Cadastrar_ComCpfJaExistente_NaoPersisteGarcom()
    {
        // Arranjo
        Garcom garcomExistente = new Garcom(
            "João",
            "(49) 99999-9999",
            "123.456.789-09"
        );

        Mock<IRepositorioGarcom> repositorioGarcom = new();

        repositorioGarcom
            .Setup(r => r.SelecionarTodos())
            .Returns([garcomExistente]);

        ServicoGarcom servicoGarcom = new(
            repositorioGarcom.Object
        );

        // Ação
        Result resultado = servicoGarcom.Cadastrar(
            new GarcomDtos.CadastrarGarcomDto(
                "Pedro",
                "(49) 98888-8888",
                "123.456.789-09"
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Já existe",
            resultado.Errors.Single().Message
        );

        repositorioGarcom.Verify(
            r => r.Cadastrar(It.IsAny<Garcom>()),
            Times.Never
        );
    }


    [TestMethod]
    public void Editar_ComDadosValidos_PersisteGarcom()
    {
        // Arranjo
        Garcom garcomExistente = new Garcom(
            "João",
            "(49) 99999-9999",
            "123.456.789-09"
        );

        Garcom? garcomAtualizado = null;

        Mock<IRepositorioGarcom> repositorioGarcom = new();

        repositorioGarcom
            .Setup(r => r.SelecionarTodos())
            .Returns([garcomExistente]);

        repositorioGarcom
            .Setup(r => r.Editar(
                garcomExistente.Id,
                It.IsAny<Garcom>()
            ))
            .Callback<Guid, Garcom>((id, garcom) =>
            {
                garcomAtualizado = garcom;
            })
            .Returns(true);

        ServicoGarcom servicoGarcom = new(
            repositorioGarcom.Object
        );

        // Ação
        Result resultado = servicoGarcom.Editar(
            new GarcomDtos.EditarGarcomDto(
                garcomExistente.Id,
                "Pedro",
                "(49) 98888-8888",
                "987.654.321-00"
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsSuccess);

        Assert.IsNotNull(garcomAtualizado);

        Assert.AreEqual(
            "Pedro",
            garcomAtualizado.Nome
        );

        Assert.AreEqual(
            "(49) 98888-8888",
            garcomAtualizado.Telefone
        );

        Assert.AreEqual(
            "987.654.321-00",
            garcomAtualizado.Cpf
        );

        repositorioGarcom.Verify(
            r => r.Editar(
                garcomExistente.Id,
                It.IsAny<Garcom>()
            ),
            Times.Once
        );
    }


    [TestMethod]
    public void Editar_ComTelefoneJaExistente_NaoPersisteGarcom()
    {
        // Arranjo
        Garcom garcomExistente = new Garcom(
            "João",
            "(49) 99999-9999",
            "123.456.789-09"
        );

        Garcom outroGarcom = new Garcom(
            "Pedro",
            "(49) 98888-8888",
            "987.654.321-00"
        );

        Mock<IRepositorioGarcom> repositorioGarcom = new();

        repositorioGarcom
            .Setup(r => r.SelecionarTodos())
            .Returns([garcomExistente, outroGarcom]);

        ServicoGarcom servicoGarcom = new(
            repositorioGarcom.Object
        );

        // Ação
        Result resultado = servicoGarcom.Editar(
            new GarcomDtos.EditarGarcomDto(
                garcomExistente.Id,
                "João Atualizado",
                outroGarcom.Telefone,
                "111.222.333-44"
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Já existe",
            resultado.Errors.Single().Message
        );

        repositorioGarcom.Verify(
            r => r.Editar(
                garcomExistente.Id,
                It.IsAny<Garcom>()
            ),
            Times.Never
        );
    }


    [TestMethod]
    public void Editar_ComCpfJaExistente_NaoPersisteGarcom()
    {
        // Arranjo
        Garcom garcomExistente = new Garcom(
            "João",
            "(49) 99999-9999",
            "123.456.789-09"
        );

        Garcom outroGarcom = new Garcom(
            "Pedro",
            "(49) 98888-8888",
            "987.654.321-00"
        );

        Mock<IRepositorioGarcom> repositorioGarcom = new();

        repositorioGarcom
            .Setup(r => r.SelecionarTodos())
            .Returns([garcomExistente, outroGarcom]);

        ServicoGarcom servicoGarcom = new(
            repositorioGarcom.Object
        );

        // Ação
        Result resultado = servicoGarcom.Editar(
            new GarcomDtos.EditarGarcomDto(
                garcomExistente.Id,
                "João Atualizado",
                "(49) 97777-7777",
                outroGarcom.Cpf
            )
        );

        // Asserção
        Assert.IsTrue(resultado.IsFailed);

        Assert.Contains(
            "Já existe",
            resultado.Errors.Single().Message
        );

        repositorioGarcom.Verify(
            r => r.Editar(
                garcomExistente.Id,
                It.IsAny<Garcom>()
            ),
            Times.Never
        );
    }


    [TestMethod]
    public void Excluir_GarcomSemContasVinculadas_ExcluiGarcom()
    {
        // Arranjo
        Garcom garcom = new Garcom(
            "João",
            "(49) 99999-9999",
            "123.456.789-09"
        );

        Mock<IRepositorioGarcom> repositorioGarcom = new();

        // Mock<IRepositorioConta> repositorioConta = new();

        repositorioGarcom
            .Setup(r => r.SelecionarPorId(garcom.Id))
            .Returns(garcom);

        // Quando o módulo Conta existir:
        //
        // repositorioConta
        //     .Setup(r => r.SelecionarTodos())
        //     .Returns([]);

        repositorioGarcom
            .Setup(r => r.Excluir(garcom.Id))
            .Returns(true);

        ServicoGarcom servicoGarcom = new(
            repositorioGarcom.Object
        // repositorioConta.Object
        );

        // Ação
        Result resultado = servicoGarcom.Excluir(garcom.Id);

        // Asserção
        Assert.IsTrue(resultado.IsSuccess);

        repositorioGarcom.Verify(
            r => r.Excluir(garcom.Id),
            Times.Once
        );
    }


    [TestMethod]
    public void PesquisarPorNome_GarcomExistente_RetornaGarcom()
    {
        // Arranjo
        Garcom garcom = new(
            "João",
            "(49) 99999-9999",
            "123.456.789-00"
        );

        Mock<IRepositorioGarcom> repositorioGarcom = new();

        repositorioGarcom
            .Setup(r => r.Filtrar(It.IsAny<Func<Garcom, bool>>()))
            .Returns((Func<Garcom, bool> filtro) =>
                new List<Garcom> { garcom }
                    .Where(filtro)
                    .ToList());

        ServicoGarcom servicoGarcom = new(
            repositorioGarcom.Object
        );

        // Ação
        List<ListarGarconsDto> resultado =
            servicoGarcom.PesquisarPorNome("João");

        // Asserção
        Assert.IsNotNull(resultado);
        Assert.HasCount(1, resultado);

        Assert.AreEqual(
            garcom.Id,
            resultado[0].Id
        );

        Assert.AreEqual(
            garcom.Nome,
            resultado[0].Nome
        );

        Assert.AreEqual(
            garcom.Telefone,
            resultado[0].Telefone
        );

        Assert.AreEqual(
            garcom.Cpf,
            resultado[0].Cpf
        );
    }


    [TestMethod]
    public void PesquisarPorNome_GarcomInexistente_RetornaListaVazia()
    {
        // Arranjo
        Mock<IRepositorioGarcom> repositorioGarcom = new();

        repositorioGarcom
            .Setup(r => r.Filtrar(It.IsAny<Func<Garcom, bool>>()))
            .Returns((Func<Garcom, bool> filtro) =>
                new List<Garcom>()
                    .Where(filtro)
                    .ToList());

        ServicoGarcom servicoGarcom = new(
            repositorioGarcom.Object
        );

        // Ação
        List<ListarGarconsDto> resultado =
            servicoGarcom.PesquisarPorNome("João");

        // Asserção
        Assert.IsNotNull(resultado);
        Assert.HasCount(0, resultado);
    }
    /*
    =========================================================
    PESQUISA DE CONTAS DO GARÇOM
    =========================================================

    Ainda não implementar enquanto o módulo Conta não existir.

    Exemplo da ideia:

    [TestMethod]
    public void PesquisarContas_GarcomAssociado_RetornaContas()
    {
        // Mock<IRepositorioConta> repositorioConta = new();

        // Conta conta1 = ...
        // Conta conta2 = ...

        // repositorioConta
        //     .Setup(r => r.Filtrar(It.IsAny<Func<Conta, bool>>()))
        //     .Returns((Func<Conta, bool> filtro) =>
        //         new List<Conta>
        //         {
        //             conta1,
        //             conta2
        //         }
        //         .Where(filtro)
        //         .ToList());

        // ServicoGarcom servicoGarcom = new(
        //     repositorioGarcom.Object,
        //     repositorioConta.Object
        // );

        // Result<List<...>> resultado =
        //     servicoGarcom.PesquisarContas(garcom.Id);

        // Assert.IsTrue(resultado.IsSuccess);
    }


    =========================================================
    EXCLUSÃO COM CONTAS
    =========================================================

    Também ficará para quando Conta existir.

    [TestMethod]
    public void Excluir_ComContasVinculadas_NaoExcluiGarcom()
    {
        // Mock<IRepositorioConta> repositorioConta = new();

        // Conta conta = ...

        // repositorioConta
        //     .Setup(r => r.Filtrar(It.IsAny<Func<Conta, bool>>()))
        //     .Returns([conta]);

        // ServicoGarcom servicoGarcom = new(
        //     repositorioGarcom.Object,
        //     repositorioConta.Object
        // );

        // Result resultado =
        //     servicoGarcom.Excluir(garcom.Id);

        // Assert.IsTrue(resultado.IsFailed);

        // repositorioGarcom.Verify(
        //     r => r.Excluir(garcom.Id),
        //     Times.Never
        // );
    }
    */
}
