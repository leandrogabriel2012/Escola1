using Escola1.Models;
using Escola1.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace Escola1.Services;

public class SalaService : ISalaService
{
    private const string apiEndPoint = "salas/";
    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;

    private IEnumerable<SalaViewModel> salasVM;
    private SalaViewModel salaVM;

    public SalaService(IHttpClientFactory clientFactory)
    {
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<SalaViewModel>> GetSalasAsync()
    {
        var client = _clientFactory.CreateClient("API");
        using (var response = await client.GetAsync(apiEndPoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                salasVM = await JsonSerializer
                    .DeserializeAsync<IEnumerable<SalaViewModel>>
                    (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return salasVM;
    }

    public async Task<SalaViewModel> GetSalaAsync(int id)
    {
        var client = _clientFactory.CreateClient("API");
        using (var response = await client.GetAsync(apiEndPoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                salaVM = await JsonSerializer
                    .DeserializeAsync<SalaViewModel>
                    (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return salaVM;
    }

    public async Task<SalaViewModel> CriarSalaAsync(SalaViewModel salaVM)
    {
        var client = _clientFactory.CreateClient("API");
        var sala = JsonSerializer.Serialize(salaVM);
        StringContent content = new StringContent(sala, Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndPoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                this.salaVM = await JsonSerializer
                    .DeserializeAsync<SalaViewModel>
                    (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return this.salaVM;
    }

    public async Task<bool> AtualizarSalaAsync(int id, SalaViewModel salaViewModel)
    {
        var client = _clientFactory.CreateClient("API");

        using (var response = await client.PutAsJsonAsync(apiEndPoint + id, salaViewModel))
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

    public async Task<bool> DeletarSalaAsync(int id)
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
