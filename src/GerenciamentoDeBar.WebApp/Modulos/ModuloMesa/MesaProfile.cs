using System;
using AutoMapper;
using static GerenciamentoDeBar.Aplicacao.Modulos.ModuloMesa.MesaDtos;
using static GerenciamentoDeBar.WebApp.Modulos.ModuloMesa.MesaViewModel;

namespace GerenciamentoDeBar.WebApp.Modulos.ModuloMesa;

public class MesaProfile : Profile
{
    public MesaProfile()
    {
        CreateMap<ListarMesasDto, ListarMesasViewModel>();
        CreateMap<DetalhesMesaDto, ListarMesasViewModel>();
        CreateMap<CadastrarMesaViewModel, CadastrarMesaDto>();
        CreateMap<EditarMesaViewModel, EditarMesaDto>();
        CreateMap<DetalhesMesaDto, EditarMesaViewModel>();
        CreateMap<DetalhesMesaDto, ExcluirMesaViewModel>();
    }
}
