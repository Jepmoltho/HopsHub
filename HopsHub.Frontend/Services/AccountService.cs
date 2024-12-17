using HopsHub.Frontend.Services.Interfaces;
using HopsHub.Shared;
using HopsHub.Shared.DTOs;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;

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

    public async Task<UserResult> LoginUserAsync(LoginDTO loginDTO)
    {
        var response = await _httpClient.PostAsJsonAsync("Login", loginDTO);

        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadFromJsonAsync<UserResult>();
            return responseData ?? throw new Exception("Invalid response data.");
        }

        var errorMessage = await response.Content.ReadAsStringAsync();
        throw new Exception($"Login failed: {errorMessage}");
    }
}

