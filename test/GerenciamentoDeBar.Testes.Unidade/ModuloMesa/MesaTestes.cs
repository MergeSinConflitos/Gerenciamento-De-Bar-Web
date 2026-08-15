using GerenciamentoDeBar.Dominio.Modulos.ModuloMesa;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GerenciamentoDeBar.Testes.Unidade.ModuloMesa;

[TestClass]
public class MesaTestes
{
    [TestMethod]
    public void Validar_ComDadosValidos_NaoRetornaErro()
    {
        //Arranjo
        Mesa mesa = new Mesa(1, 4);

        //Ação
        List<string> erros = mesa.Validar();

        //Asserção
        Assert.HasCount(0, erros);

    }

    [TestMethod]
    public void Validar_ComQuantidadeDeLugarZero_RetornaErro()
    {

        //Arranjo
        Mesa mesa = new Mesa(1, 0);

        //Ação
        List<string> erros = mesa.Validar();

        //Asserção
        Assert.HasCount(1, erros);
        Assert.AreEqual("Informe um número positivo", erros.First());
    }



    [TestMethod]
    public void Validar_ComQuantidadeDeLugarNegativa_RetornaErro()
    {

        //Arranjo
        Mesa mesa = new Mesa(1, -5);

        //Ação
        List<string> erros = mesa.Validar();

        //Asserção
        Assert.HasCount(1, erros);
        Assert.AreEqual("Informe um número positivo", erros.First());
    }

    [TestMethod]
    public void Validar_ComNumeroNegativo_RetornaErro()
    {

        //Arranjo
        Mesa mesa = new Mesa(-1, 2);

        //Ação
        List<string> erros = mesa.Validar();

        //Asserção
        Assert.HasCount(1, erros);
        Assert.AreEqual("Informe um número positivo", erros.First());
    }

    [TestMethod]
    public void Validar_ComNumeroIgualZero_RetornaErro()
    {

        //Arranjo
        Mesa mesa = new Mesa(0, 4);

        //Ação
        List<string> erros = mesa.Validar();

        //Asserção
        Assert.HasCount(1, erros);
        Assert.AreEqual("Informe um número positivo", erros.First());
    }


    [TestMethod]
    public void Validar_ComQuantidadeDeLugarAcimaDoLimite_RetornaErro()
    {

        //Arranjo
        Mesa mesa = new Mesa(1, 11);

        //Ação
        List<string> erros = mesa.Validar();

        //Asserção
        Assert.HasCount(1, erros);
        Assert.AreEqual("A quantidade máxima é de 10 lugares", erros.First());
    }


    [TestMethod]
    public void Atualizar_ComDadosValidos_DeveAtualizarTodosOsDados()
    {
        //Arranjo
        Mesa mesa = new Mesa(1, 4);

        Mesa mesaAtualizada = new Mesa(5, 2);

        //Ação
        mesa.Atualizar(mesaAtualizada);

        //Asserção
        Assert.AreEqual(5, mesa.NumeroDaMesa);
        Assert.AreEqual(2, mesa.QuantidadeDeLugares);

    }
}
