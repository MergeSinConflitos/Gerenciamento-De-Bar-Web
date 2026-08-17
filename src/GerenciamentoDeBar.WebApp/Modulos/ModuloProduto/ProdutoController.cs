namespace GerenciamentoDeBar.WebApp.Modulos.ModuloProduto
{
    using AutoMapper;
    using FluentResults;
    using global::GerenciamentoDeBar.Aplicacao.Modulos.ModuloProduto;
    using global::GerenciamentoDeBar.WebApp.Compartilhado.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using static global::GerenciamentoDeBar.Aplicacao.Modulos.ModuloProduto.ProdutoDtos;
    using static global::GerenciamentoDeBar.WebApp.Modulos.ModuloProduto.ProdutoViewModel;

    public class ProdutoController(
        ServicoProduto servicoProduto,
        IMapper mapeador
    ) : Controller
    {
        [HttpGet]
        public ActionResult Listar(string? nome)
        {
            if (!string.IsNullOrWhiteSpace(nome))
            {
                List<ListarProdutosDto> dtos =
                    servicoProduto.PesquisarPorNome(nome);

                List<ListarProdutosViewModel> listarVms =
                    mapeador.Map<List<ListarProdutosViewModel>>(dtos);

                return View(listarVms);
            }

            List<ListarProdutosDto> todos =
                servicoProduto.SelecionarTodos();

            List<ListarProdutosViewModel> vms =
                mapeador.Map<List<ListarProdutosViewModel>>(todos);

            return View(vms);
        }


        [HttpGet]
        public ActionResult Cadastrar()
        {
            CadastrarProdutoViewModel cadastrarVm = new(
                string.Empty,
                0
            );

            return View(cadastrarVm);
        }


        [HttpPost]
        public ActionResult Cadastrar(
            CadastrarProdutoViewModel cadastrarVm
        )
        {
            if (!ModelState.IsValid)
                return View(cadastrarVm);

            CadastrarProdutoDto dto =
                mapeador.Map<CadastrarProdutoDto>(cadastrarVm);

            Result resultado =
                servicoProduto.Cadastrar(dto);

            if (resultado.IsFailed)
            {
                ModelState.AddModelError(resultado);
                return View(cadastrarVm);
            }

            return RedirectToAction(nameof(Listar));
        }


        [HttpGet]
        public ActionResult Editar(Guid id)
        {
            Result<DetalhesProdutoDto> resultado =
                servicoProduto.SelecionarPorId(id);

            if (resultado.IsFailed)
            {
                TempData.AddErrorMessage(resultado);
                return RedirectToAction(nameof(Listar));
            }

            EditarProdutoViewModel editarVm =
                mapeador.Map<EditarProdutoViewModel>(
                    resultado.Value
                );

            return View(editarVm);
        }


        [HttpPost]
        public ActionResult Editar(
            EditarProdutoViewModel editarVm
        )
        {
            if (!ModelState.IsValid)
                return View(editarVm);

            EditarProdutoDto dto =
                mapeador.Map<EditarProdutoDto>(editarVm);

            Result resultado =
                servicoProduto.Editar(dto);

            if (resultado.IsFailed)
            {
                ModelState.AddModelError(resultado);
                return View(editarVm);
            }

            return RedirectToAction(nameof(Listar));
        }


        [HttpGet]
        public ActionResult Excluir(Guid id)
        {
            Result<DetalhesProdutoDto> resultado =
                servicoProduto.SelecionarPorId(id);

            if (resultado.IsFailed)
            {
                TempData.AddErrorMessage(resultado);
                return RedirectToAction(nameof(Listar));
            }

            ExcluirProdutoViewModel excluirVm =
                mapeador.Map<ExcluirProdutoViewModel>(
                    resultado.Value
                );

            return View(excluirVm);
        }


        [HttpPost]
        public ActionResult Excluir(
            ExcluirProdutoViewModel excluirVm
        )
        {
            Result resultado =
                servicoProduto.Excluir(excluirVm.Id);

            if (resultado.IsFailed)
                TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }
    }
}