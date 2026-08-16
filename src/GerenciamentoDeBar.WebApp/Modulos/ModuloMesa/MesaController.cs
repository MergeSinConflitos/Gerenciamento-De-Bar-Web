namespace GerenciamentoDeBar.WebApp.Modulos.ModuloMesa
{
    using AutoMapper;
    using FluentResults;
    using global::GerenciamentoDeBar.Aplicacao.Modulos.ModuloMesa;
    using global::GerenciamentoDeBar.Dominio.Modulos.ModuloMesa;
    using global::GerenciamentoDeBar.WebApp.Compartilhado.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using static global::GerenciamentoDeBar.Aplicacao.Modulos.ModuloMesa.MesaDtos;
    using static global::GerenciamentoDeBar.WebApp.Modulos.ModuloMesa.MesaViewModel;

    namespace GerenciamentoDeBar.WebApp.Controllers
    {
        public class MesaController(ServicoMesa servicoMesa, IMapper mapeador) : Controller
        {
            [HttpGet]
            public ActionResult Listar(int? numeroDaMesa, StatusMesa? status)
            {
                if (numeroDaMesa.HasValue)
                {
                    Result<DetalhesMesaDto> resultado =
                        servicoMesa.PesquisarPorNumero(numeroDaMesa.Value);

                    if (resultado.IsFailed)
                    {
                        TempData.AddErrorMessage(resultado);
                        return View(new List<ListarMesasViewModel>());
                    }

                    ListarMesasViewModel mesaVm =
                        mapeador.Map<ListarMesasViewModel>(resultado.Value);

                    return View(new List<ListarMesasViewModel> { mesaVm });
                }

                if (status.HasValue)
                {
                    List<ListarMesasDto> dtos =
                        servicoMesa.PesquisarPorStatus(status.Value);

                    List<ListarMesasViewModel> listarVms =
                        mapeador.Map<List<ListarMesasViewModel>>(dtos);

                    return View(listarVms);
                }

                List<ListarMesasDto> todos =
                    servicoMesa.SelecionarTodos();

                List<ListarMesasViewModel> vms =
                    mapeador.Map<List<ListarMesasViewModel>>(todos);

                return View(vms);
            }

            [HttpGet]
            public ActionResult Cadastrar()
            {
                CadastrarMesaViewModel cadastrarVm = new(
                    0,
                    0
                );

                return View(cadastrarVm);
            }

            [HttpPost]
            public ActionResult Cadastrar(CadastrarMesaViewModel cadastrarVm)
            {
                if (!ModelState.IsValid)
                    return View(cadastrarVm);

                CadastrarMesaDto dto =
                    mapeador.Map<CadastrarMesaDto>(cadastrarVm);

                Result resultado = servicoMesa.Cadastrar(dto);

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
                Result<DetalhesMesaDto> resultado =
                    servicoMesa.SelecionarPorId(id);

                if (resultado.IsFailed)
                {
                    TempData.AddErrorMessage(resultado);
                    return RedirectToAction(nameof(Listar));
                }

                EditarMesaViewModel editarVm =
                    mapeador.Map<EditarMesaViewModel>(resultado.Value);

                return View(editarVm);
            }

            [HttpPost]
            public ActionResult Editar(EditarMesaViewModel editarVm)
            {
                if (!ModelState.IsValid)
                    return View(editarVm);

                EditarMesaDto dto =
                    mapeador.Map<EditarMesaDto>(editarVm);

                Result resultado = servicoMesa.Editar(dto);

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
                Result<DetalhesMesaDto> resultado =
                    servicoMesa.SelecionarPorId(id);

                if (resultado.IsFailed)
                {
                    TempData.AddErrorMessage(resultado);
                    return RedirectToAction(nameof(Listar));
                }

                ExcluirMesaViewModel excluirVm =
                    mapeador.Map<ExcluirMesaViewModel>(resultado.Value);

                return View(excluirVm);
            }

            [HttpPost]
            public ActionResult Excluir(ExcluirMesaViewModel excluirVm)
            {
                Result resultado = servicoMesa.Excluir(excluirVm.Id);

                if (resultado.IsFailed)
                    TempData.AddErrorMessage(resultado);

                return RedirectToAction(nameof(Listar));
            }
        }

    }
}
