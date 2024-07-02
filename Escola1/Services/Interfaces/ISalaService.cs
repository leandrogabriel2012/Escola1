using Escola1.Models;

namespace Escola1.Services.Interfaces;

public interface ISalaService
{
    Task<IEnumerable<SalaViewModel>> GetSalasAsync();
    Task<SalaViewModel> GetSalaAsync(int id);
    Task<SalaViewModel> CriarSalaAsync(SalaViewModel salaVM);
    Task<bool> AtualizarSalaAsync(int id, SalaViewModel salaVM);
    Task<bool> DeletarSalaAsync(int id);
}
