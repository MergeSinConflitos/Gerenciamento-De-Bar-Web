using System;
using AutoMapper;
using static GerenciamentoDeBar.Aplicacao.Modulos.ModuloGarcom.GarcomDtos;
using static GerenciamentoDeBar.WebApp.Modulos.ModuloGarcom.GarcomViewModel;

namespace GerenciamentoDeBar.WebApp.Modulos.ModuloGarcom;

public class GarcomProfile : Profile
{
    public GarcomProfile()
    {
        CreateMap<ListarGarconsDto, ListarGarconsViewModel>();
        CreateMap<DetalhesGarcomDto, ListarGarconsViewModel>();
        CreateMap<CadastrarGarcomViewModel, CadastrarGarcomDto>();
        CreateMap<EditarGarcomViewModel, EditarGarcomDto>();
        CreateMap<DetalhesGarcomDto, EditarGarcomViewModel>();
        CreateMap<DetalhesGarcomDto, ExcluirGarcomViewModel>();
    }
}
