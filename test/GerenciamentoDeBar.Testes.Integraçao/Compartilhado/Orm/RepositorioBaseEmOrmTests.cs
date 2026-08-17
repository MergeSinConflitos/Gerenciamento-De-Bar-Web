using GerenciamentoDeBar.Infra.Compartilhado.Orm;
using GerenciamentoDeBar.Testes.Integraçao.Compartilhado.Identity;
using Microsoft.EntityFrameworkCore;
using GerenciamentoDeBar.Dominio.Modulos.ModuloMesa;
using GerenciamentoDeBar.Infra.Modulos.ModuloMesa;
using FizzWare.NBuilder;
using GerenciamentoDeBar.Dominio.Modulos.ModuloGarcom;
using GerenciamentoDeBar.Infra.Modulos.ModuloGarcom;
using GerenciamentoDeBar.Infra.Modulos.ModuloProduto;
using GerenciamentoDeBar.Dominio.Modulos.ModuloProduto;
// using GerenciamentoDeBar.Dominio.Modulos.ModuloProduto;
// using GerenciamentoDeBar.Dominio.Modulos.ModuloConta;
// using GerenciamentoDeBar.Dominio.Modulos.ModuloPedido;
// using GerenciamentoDeBar.Infra.Modulos.ModuloMesa;
// using GerenciamentoDeBar.Infra.Modulos.ModuloGarcom;
// using GerenciamentoDeBar.Infra.Modulos.ModuloProduto;
// using GerenciamentoDeBar.Infra.Modulos.ModuloConta;
// using GerenciamentoDeBar.Infra.Modulos.ModuloPedido;

namespace GerenciamentoDeBar.Testes.Integracao.Compartilhado;


public abstract class RepositorioBaseEmOrmTests
{
    protected GerenciamentoDeBarDbContext dbContext = null!;


    // =========================================================
    // REPOSITÓRIOS
    // =========================================================
    // Habilitar conforme os módulos forem sendo implementados.


    protected RepositorioMesaEmOrm repositorioMesa = null!;
    protected RepositorioGarcomEmOrm repositorioGarcom = null!;
    protected RepositorioProdutoEmOrm repositorioProduto = null!;
    //protected RepositorioContaEmOrm repositorioConta = null!;
    //protected RepositorioPedidoEmOrm repositorioPedido = null!;



    // =========================================================
    // INICIALIZAÇÃO
    // =========================================================

    [TestInitialize]
    public void InicializarContexto()
    {
        dbContext = CriarDbContext(Guid.NewGuid());


        // =====================================================
        // MESA
        // =====================================================
        // Habilitar quando o módulo Mesa estiver implementado.


        repositorioMesa = new RepositorioMesaEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Mesa>(
            repositorioMesa.Cadastrar
        );

        BuilderSetup.SetCreatePersistenceMethod<IList<Mesa>>(
            mesas =>
            {
                foreach (Mesa mesa in mesas)
                    repositorioMesa.Cadastrar(mesa);
            }
        );



        // =====================================================
        // GARÇOM
        // =====================================================
        // Habilitar quando o módulo Garcom estiver implementado.


        repositorioGarcom = new RepositorioGarcomEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Garcom>(
            repositorioGarcom.Cadastrar
        );

        BuilderSetup.SetCreatePersistenceMethod<IList<Garcom>>(
            garcons =>
            {
                foreach (Garcom garcom in garcons)
                    repositorioGarcom.Cadastrar(garcom);
            }
        );



        // =====================================================
        // PRODUTO
        // =====================================================
        // Habilitar quando o módulo Produto estiver implementado.


        repositorioProduto = new RepositorioProdutoEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Produto>(
            repositorioProduto.Cadastrar
        );

        BuilderSetup.SetCreatePersistenceMethod<IList<Produto>>(
            produtos =>
            {
                foreach (Produto produto in produtos)
                    repositorioProduto.Cadastrar(produto);
            }
        );



        // =====================================================
        // CONTA
        // =====================================================
        // Habilitar quando o módulo Conta estiver implementado.

        /*
        repositorioConta = new RepositorioContaEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Conta>(
            repositorioConta.Cadastrar
        );

        BuilderSetup.SetCreatePersistenceMethod<IList<Conta>>(
            contas =>
            {
                foreach (Conta conta in contas)
                    repositorioConta.Cadastrar(conta);
            }
        );
        */


        // =====================================================
        // PEDIDO / ITEM PEDIDO
        // =====================================================
        // Habilitar quando o módulo Pedido estiver implementado.

        /*
        repositorioPedido = new RepositorioPedidoEmOrm(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Pedido>(
            repositorioPedido.Cadastrar
        );

        BuilderSetup.SetCreatePersistenceMethod<IList<Pedido>>(
            pedidos =>
            {
                foreach (Pedido pedido in pedidos)
                    repositorioPedido.Cadastrar(pedido);
            }
        );
        */
    }


    // =========================================================
    // LIMPEZA
    // =========================================================

    [TestCleanup]
    public void DescartarContexto()
    {
        dbContext.Dispose();
    }


    // =========================================================
    // CRIAÇÃO DO CONTEXTO
    // =========================================================

    private static GerenciamentoDeBarDbContext CriarDbContext(
        Guid userId
    )
    {
        DbContextOptions<GerenciamentoDeBarDbContext> options =
            new DbContextOptionsBuilder<GerenciamentoDeBarDbContext>()
                .UseInMemoryDatabase(
                    $"integracao-{Guid.NewGuid():N}"
                )
                .Options;

        return new GerenciamentoDeBarDbContext(
            options,
            new ProvedorDeUsuarioFake(userId)
        );
    }
}