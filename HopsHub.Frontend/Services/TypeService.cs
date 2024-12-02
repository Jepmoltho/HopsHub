using HopsHub.Frontend.Services.Interfaces;
using HopsHub.Shared;
using HopsHub.Shared.DTOs;
using System.Net.Http.Json;

namespace HopsHub.Frontend.Services;


public class TypeService : ITypeService
{
    private HttpClient _httpClient;

    public TypeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TypeDTO>> GetTypesAsync()
    {
        var response = await _httpClient.GetAsync("Types");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<List<TypeDTO>>();

            return result ?? new List<TypeDTO>();
        }

        throw new Exception($"Failed to fetch types. Status code: {response.StatusCode}");
    }
}
