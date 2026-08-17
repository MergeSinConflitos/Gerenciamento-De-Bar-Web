using System;
using AutoMapper;
using static GerenciamentoDeBar.Aplicacao.Modulos.ModuloProduto.ProdutoDtos;
using static GerenciamentoDeBar.WebApp.Modulos.ModuloProduto.ProdutoViewModel;

namespace GerenciamentoDeBar.WebApp.Modulos.ModuloProduto;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<ListarProdutosDto, ListarProdutosViewModel>();
        CreateMap<DetalhesProdutoDto, ListarProdutosViewModel>();
        CreateMap<CadastrarProdutoViewModel, CadastrarProdutoDto>();
        CreateMap<EditarProdutoViewModel, EditarProdutoDto>();
        CreateMap<DetalhesProdutoDto, EditarProdutoViewModel>();
        CreateMap<DetalhesProdutoDto, ExcluirProdutoViewModel>();
    }
}
