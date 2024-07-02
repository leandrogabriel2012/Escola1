using Escola1.Models;
using Escola1.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace Escola1.Services;

public class TurmaService : ITurmaService
{
    private const string apiEndPoint = "turmas/";
    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;

    private IEnumerable<TurmaViewModel> turmasVM;
    private TurmaViewModel turmaVM;

    public TurmaService(IHttpClientFactory clientFactory)
    {
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<TurmaViewModel>> GetTurmasAsync()
    {
        var client = _clientFactory.CreateClient("API");
        using (var response = await client.GetAsync(apiEndPoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                turmasVM = await JsonSerializer
                    .DeserializeAsync<IEnumerable<TurmaViewModel>>
                    (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return turmasVM;
    }

    public async Task<TurmaViewModel> GetTurmaAsync(int id)
    {
        var client = _clientFactory.CreateClient("API");
        using (var response = await client.GetAsync(apiEndPoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                turmaVM = await JsonSerializer
                    .DeserializeAsync<TurmaViewModel>
                    (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return turmaVM;
    }

    public async Task<TurmaViewModel> CriarTurmaAsync(TurmaViewModel turmaVM)
    {
        var client = _clientFactory.CreateClient("API");
        var turma = JsonSerializer.Serialize(turmaVM);
        StringContent content = new StringContent(turma, Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndPoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                turmaVM = await JsonSerializer
                    .DeserializeAsync<TurmaViewModel>
                    (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return turmaVM;
    }

    public async Task<bool> AtualizarTurmaAsync(int id, TurmaViewModel turmaVM)
    {
        var client = _clientFactory.CreateClient("API");

        using (var response = await client.PutAsJsonAsync(apiEndPoint + id, turmaVM))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public async Task<bool> DeletarTurmaAsync(int id)
    {
        var client = _clientFactory.CreateClient("API");

        using (var response = await client.DeleteAsync(apiEndPoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }

        return false;
    }
}
