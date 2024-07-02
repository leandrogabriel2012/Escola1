using Escola1.Models;

namespace Escola1.Services.Interfaces;

public interface ITurmaService
{
    Task<IEnumerable<TurmaViewModel>> GetTurmasAsync();
    Task<TurmaViewModel> GetTurmaAsync(int id);
    Task<TurmaViewModel> CriarTurmaAsync(TurmaViewModel turmaVM);
    Task<bool> AtualizarTurmaAsync(int id, TurmaViewModel turmaVM);
    Task<bool> DeletarTurmaAsync(int id);
}
