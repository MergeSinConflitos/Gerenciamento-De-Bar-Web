using System;
using GerenciamentoDeBar.Dominio.Modulos.ModuloMesa;
using GerenciamentoDeBar.Infra.Compartilhado.Orm;

namespace GerenciamentoDeBar.Infra.Modulos.ModuloMesa;

public class RepositorioMesaEmOrm(GerenciamentoDeBarDbContext dbContext) : RepositorioBaseEmOrm<Mesa>(dbContext), IRepositorioMesa;
