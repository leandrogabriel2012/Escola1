using Escola1.Models;
using Escola1.Services;
using Escola1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Escola1.Controllers;

public class TurmasController : Controller
{
    private readonly ITurmaService _turmaService;
    private readonly ISalaService _salaService;

    public TurmasController(ITurmaService turmaService, ISalaService salaService)
    {
        _turmaService = turmaService;
        _salaService = salaService;
    }

    [HttpGet]
    public async Task<ActionResult<TurmaViewModel>> Index()
    {
        var result = await _turmaService.GetTurmasAsync();

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpGet]
    public async Task<ActionResult<TurmaViewModel>> Detalhes(int id)
    {
        var result = await _turmaService.GetTurmaAsync(id);

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpGet]
    public async Task<ActionResult> Criar()
    {
        ViewBag.SalaId =
                new SelectList(await _salaService.GetSalasAsync(), "SalaId", "Numero");

        return View();
    }

    [HttpPost]
    public async Task<ActionResult<TurmaViewModel>> Criar(TurmaViewModel turmaVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _turmaService.CriarTurmaAsync(turmaVM);

            if (result is not null)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        ViewBag.Erro = "Erro ao inserir turma!";
        return View(turmaVM);
    }

    [HttpGet]
    public async Task<ActionResult> Atualizar(int id)
    {
        var turma = await _turmaService.GetTurmaAsync(id);

        if (turma is null)
            return View("Error");

        ViewBag.SalaId =
                new SelectList(await _salaService.GetSalasAsync(), "SalaId", "Numero");

        return View(turma);
    }

    [HttpPost]
    public async Task<ActionResult> Atualizar(int id, TurmaViewModel turmaVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _turmaService.AtualizarTurmaAsync(id, turmaVM);

            if (result)
                return RedirectToAction(nameof(Index));
        }

        ViewBag.Erro = "Não foi possível atualizar turma!";
        return View(turmaVM);
    }

    [HttpGet]
    public async Task<ActionResult> Deletar(int id)
    {
        var result = await _turmaService.GetTurmaAsync(id);

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost]
    public async Task<ActionResult> DeletarTurma(int turmaId)
    {
        var result = await _turmaService.DeletarTurmaAsync(turmaId);

        if (result)
            return RedirectToAction("Index");

        return View("Error");
    }
}
