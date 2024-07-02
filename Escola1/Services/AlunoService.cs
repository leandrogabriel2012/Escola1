using Escola1.Models;
using Escola1.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace Escola1.Services;

public class AlunoService : IAlunoService
{
    private const string apiEndPoint = "alunos/";
    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;

    private IEnumerable<AlunoViewModel> alunosVM;
    private AlunoViewModel alunoVM;

    public AlunoService(IHttpClientFactory clientFactory)
    {
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<AlunoViewModel>> GetAlunosAsync()
    {
        var client = _clientFactory.CreateClient("API");
        using (var response = await client.GetAsync(apiEndPoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                alunosVM = await JsonSerializer
                    .DeserializeAsync<IEnumerable<AlunoViewModel>>
                    (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return alunosVM;
    }

    public async Task<AlunoViewModel> GetAlunoAsync(int id)
    {
        var client = _clientFactory.CreateClient("API");
        using (var response = await client.GetAsync(apiEndPoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                alunoVM = await JsonSerializer
                    .DeserializeAsync<AlunoViewModel>
                    (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return alunoVM;
    }

    public async Task<AlunoViewModel> CriarAlunoAsync(AlunoViewModel alunoVM)
    {
        var client = _clientFactory.CreateClient("API");
        var aluno = JsonSerializer.Serialize(alunoVM);
        StringContent content = new StringContent(aluno, Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndPoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                alunoVM = await JsonSerializer
                    .DeserializeAsync<AlunoViewModel>
                    (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return alunoVM;
    }

    public async Task<bool> AtualizarAlunoAsync(int id, AlunoViewModel alunoVM)
    {
        var client = _clientFactory.CreateClient("API");

        using (var response = await client.PutAsJsonAsync(apiEndPoint + id, alunoVM))
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

    public async Task<bool> DeletarAlunoAsync(int id)
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
