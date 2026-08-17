using GerenciamentoDeBar.Dominio.Modulos.ModuloGarcom;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GerenciamentoDeBar.Testes.Unidade.Modulos.ModuloGarcom;

[TestClass]
public class GarcomTestes
{
    [TestMethod]
    public void Validar_ComDadosValidos_NaoRetornaErro()
    {
        // Arranjo
        Garcom garcom = new Garcom(
            "João",
            "(49) 99999-9999",
            "123.456.789-09"
        );

        // Ação
        List<string> erros = garcom.Validar();

        // Asserção
        Assert.HasCount(0, erros);
    }


    [TestMethod]
    public void Validar_ComNomeVazio_RetornaErro()
    {
        // Arranjo
        Garcom garcom = new Garcom(
            "",
            "(49) 99999-9999",
            "123.456.789-09"
        );

        // Ação
        List<string> erros = garcom.Validar();

        // Asserção
        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Nome\" deve ser preenchido",
            erros.First()
        );
    }


    [TestMethod]
    public void Validar_ComNomeAbaixaoDoLomite_RetornaErro()
    {
        // Arranjo
        Garcom garcom = new Garcom(
            "A",
            "(49) 99999-9999",
            "123.456.789-09"
        );

        // Ação
        List<string> erros = garcom.Validar();

        // Asserção
        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O nome deve ter entre 2 e 100 caracteres",
            erros.First()
        );
    }


    [TestMethod]
    public void Validar_ComNomeAcimaDoLimite_RetornaErro()
    {
        // Arranjo
        string nome = new string('A', 101);

        Garcom garcom = new Garcom(
            nome,
            "(49) 99999-9999",
            "123.456.789-09"
        );

        // Ação
        List<string> erros = garcom.Validar();

        // Asserção
        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O nome deve ter entre 2 e 100 caracteres",
            erros.First()
        );
    }


    [TestMethod]
    public void Validar_ComTelefoneVazio_RetornaErro()
    {
        // Arranjo
        Garcom garcom = new Garcom(
            "João",
            "",
            "123.456.789-09"
        );

        // Ação
        List<string> erros = garcom.Validar();

        // Asserção
        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"Telefone\" deve ser preenchido",
            erros.First()
        );
    }


    [TestMethod]
    public void Validar_ComTelefoneInvalido_RetornaErro()
    {
        // Arranjo
        Garcom garcom = new Garcom(
            "João",
            "123",
            "123.456.789-09"
        );

        // Ação
        List<string> erros = garcom.Validar();

        // Asserção
        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O telefone informado é inválido",
            erros.First()
        );
    }


    [TestMethod]
    public void Validar_ComCpfVazio_RetornaErro()
    {
        // Arranjo
        Garcom garcom = new Garcom(
            "João",
            "(49) 99999-9999",
            ""
        );

        // Ação
        List<string> erros = garcom.Validar();

        // Asserção
        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O campo \"CPF\" deve ser preenchido",
            erros.First()
        );
    }


    [TestMethod]
    public void Validar_ComCpfFormatoInvalido_RetornaErro()
    {
        // Arranjo
        Garcom garcom = new Garcom(
            "João",
            "(49) 99999-9999",
            "123"
        );

        // Ação
        List<string> erros = garcom.Validar();

        // Asserção
        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O CPF informado é inválido",
            erros.First()
        );
    }


    [TestMethod]
    public void Validar_ComCpfMatematicamenteInvalido_RetornaErro()
    {
        // Arranjo
        Garcom garcom = new Garcom(
            "João",
            "(49) 99999-9999",
            "123.456.789-00"
        );

        // Ação
        List<string> erros = garcom.Validar();

        // Asserção
        Assert.HasCount(1, erros);
        Assert.AreEqual(
            "O CPF informado é inválido",
            erros.First()
        );
    }


    [TestMethod]
    public void Validar_ComCpfValido_NaoRetornaErro()
    {
        // Arranjo
        Garcom garcom = new Garcom(
            "João",
            "(49) 99999-9999",
            "123.456.789-09"
        );

        // Ação
        List<string> erros = garcom.Validar();

        // Asserção
        Assert.HasCount(0, erros);
    }


    [TestMethod]
    public void Atualizar_ComDadosValidos_DeveAtualizarTodosOsDados()
    {
        // Arranjo
        Garcom garcom = new Garcom(
            "João",
            "(49) 99999-9999",
            "123.456.789-09"
        );

        Garcom garcomAtualizado = new Garcom(
            "Pedro",
            "(49) 98888-8888",
            "123.456.789-09"
        );

        // Ação
        garcom.Atualizar(garcomAtualizado);

        // Asserção
        Assert.AreEqual("Pedro", garcom.Nome);
        Assert.AreEqual("(49) 98888-8888", garcom.Telefone);
        Assert.AreEqual("123.456.789-09", garcom.Cpf);
    }
}