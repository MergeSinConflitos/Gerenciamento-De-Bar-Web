using System;
using GerenciamentoDeBar.Dominio.Modulos.ModuloProduto;
using GerenciamentoDeBar.Infra.Compartilhado.Orm;

namespace GerenciamentoDeBar.Infra.Modulos.ModuloProduto;

public class RepositorioProdutoEmOrm(GerenciamentoDeBarDbContext dbContext) : RepositorioBaseEmOrm<Produto>(dbContext), IRepositorioProduto;
