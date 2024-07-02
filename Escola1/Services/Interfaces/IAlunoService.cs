using Escola1.Models;

namespace Escola1.Services.Interfaces;

public interface IAlunoService
{
    Task<IEnumerable<AlunoViewModel>> GetAlunosAsync();
    Task<AlunoViewModel> GetAlunoAsync(int id);
    Task<AlunoViewModel> CriarAlunoAsync(AlunoViewModel alunoVM);
    Task<bool> AtualizarAlunoAsync(int id, AlunoViewModel alunoVM);
    Task<bool> DeletarAlunoAsync(int id);
}
