using HopsHub.Frontend.Services.Interfaces;
using HopsHub.Shared;
using HopsHub.Shared.DTOs;
using System.Net.Http.Json;

namespace HopsHub.Frontend.Services;


public class AccountService : IAccountService
{
	private HttpClient _httpClient;

	public AccountService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<Result> CreateUserAsync(CreateUserDTO createUserDTO) 
	{
		var response = await _httpClient.PostAsJsonAsync("CreateUser", createUserDTO);

		if (response.IsSuccessStatusCode)
		{
			var result = await response.Content.ReadFromJsonAsync<Result>();
			return result ?? new Result { Succeeded = true, Message = "Sucesfully created user. Please confirm in your email"};
		}
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error: {response.StatusCode} - {errorMessage}");
        }
    }

    public async Task<Result> LoginUserAsync(LoginDTO loginDTO)
    {
        var response = await _httpClient.PostAsJsonAsync("Login", loginDTO);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<Result>();
            return result ?? new Result { Succeeded = true, Message = "Sucesfully logged in user. Please confirm in your email" };
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error: {response.StatusCode} - {errorMessage}");
        }
    }
}

