using Escola1.Models;
using Escola1.Services;
using Escola1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Escola1.Controllers;

public class AlunosController : Controller
{
    private readonly IAlunoService _alunoService;
    private readonly ITurmaService _turmaService;

    public AlunosController(IAlunoService alunoService, ITurmaService turmaService)
    {
        _alunoService = alunoService;
        _turmaService = turmaService;
    }

    [HttpGet]
    public async Task<ActionResult<AlunoViewModel>> Index()
    {
        var result = await _alunoService.GetAlunosAsync();

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpGet]
    public async Task<ActionResult<AlunoViewModel>> Detalhes(int id)
    {
        var result = await _alunoService.GetAlunoAsync(id);

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpGet]
    public async Task<ActionResult> Criar()
    {
        ViewBag.TurmaId =
                new SelectList(await _turmaService.GetTurmasAsync(), "TurmaId", "Nome");

        return View();
    }

    [HttpPost]
    public async Task<ActionResult<AlunoViewModel>> Criar(AlunoViewModel alunoVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _alunoService.CriarAlunoAsync(alunoVM);

            if (result is not null)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        ViewBag.Erro = "Erro ao inserir aluno!";
        return View(alunoVM);
    }

    [HttpGet]
    public async Task<ActionResult> Atualizar(int id)
    {
        var aluno = await _alunoService.GetAlunoAsync(id);

        if (aluno is null)
            return View("Error");

        ViewBag.TurmaId =
                new SelectList(await _turmaService.GetTurmasAsync(), "TurmaId", "Nome");

        return View(aluno);
    }

    [HttpPost]
    public async Task<ActionResult> Atualizar(int id, AlunoViewModel alunoVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _alunoService.AtualizarAlunoAsync(id, alunoVM);

            if (result)
                return RedirectToAction(nameof(Index));
        }

        ViewBag.Erro = "Não foi possível atualizar aluno!";
        return View(alunoVM);
    }

    [HttpGet]
    public async Task<ActionResult> Deletar(int id)
    {
        var result = await _alunoService.GetAlunoAsync(id);

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost]
    public async Task<ActionResult> DeletarAluno(int alunoId)
    {
        var result = await _alunoService.DeletarAlunoAsync(alunoId);

        if (result)
            return RedirectToAction("Index");

        return View("Error");
    }
}
