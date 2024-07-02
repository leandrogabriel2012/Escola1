using Escola1.Models;
using Escola1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Escola1.Controllers
{
    public class SalasController : Controller
    {
        private readonly ISalaService _salaService;

        public SalasController(ISalaService salaService)
        {
            _salaService = salaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaViewModel>>> Index()
        {
            var result = await _salaService.GetSalasAsync();

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult<SalaViewModel>> Detalhes(int id)
        {
            var result = await _salaService.GetSalaAsync(id);

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpGet]
        public ActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<SalaViewModel>> Criar(SalaViewModel salaVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _salaService.CriarSalaAsync(salaVM);

                if (result is not null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewBag.Erro = "Erro ao inserir sala!";
            return View(salaVM);
        }


        [HttpGet]
        public async Task<ActionResult> Atualizar(int id)
        {
            var sala = await _salaService.GetSalaAsync(id);

            if (sala is null)
                return View("Error");

            return View(sala);
        }

        [HttpPost]
        public async Task<ActionResult> Atualizar(int id, SalaViewModel salaVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _salaService.AtualizarSalaAsync(id, salaVM);

                if (result)
                    return RedirectToAction(nameof(Index));
            }

            ViewBag.Erro = "Não foi possível atualizar sala!";
            return View(salaVM);
        }

        [HttpGet]
        public async Task<ActionResult> Deletar(int id)
        {
            var result = await _salaService.GetSalaAsync(id);

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult> DeletarSala(int salaId)
        {
            var result = await _salaService.DeletarSalaAsync(salaId);

            if(result)
                return RedirectToAction("Index");

            return View("Error");
        }
    }
}
