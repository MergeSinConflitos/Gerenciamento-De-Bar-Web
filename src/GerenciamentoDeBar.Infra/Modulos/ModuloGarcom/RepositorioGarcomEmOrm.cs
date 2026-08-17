using System;
using GerenciamentoDeBar.Dominio.Modulos.ModuloGarcom;
using GerenciamentoDeBar.Infra.Compartilhado.Orm;

namespace GerenciamentoDeBar.Infra.Modulos.ModuloGarcom;

public class RepositorioGarcomEmOrm(GerenciamentoDeBarDbContext dbContext) : RepositorioBaseEmOrm<Garcom>(dbContext), IRepositorioGarcom;
