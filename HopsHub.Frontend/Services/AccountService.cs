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

    private ILocalStorageService _localStorage;

    //private AuthenticationStateService _authenticationStateService;

	public AccountService(HttpClient httpClient, ILocalStorageService localStorage/*, AuthenticationStateService authenticationStateService*/)
	{
		_httpClient = httpClient;
        _localStorage = localStorage;
        //_authenticationStateService = authenticationStateService;
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
        //Step 1: Sends login body to https://localhost:8080/Login in Development
        var response = await _httpClient.PostAsJsonAsync("Login", loginDTO);

        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadFromJsonAsync<UserResult>();

            if (responseData == null)
            {
                throw new Exception("Token not returned.");
            }
            var token = responseData.Token;

            if (!string.IsNullOrEmpty(token))
            {
                // Save token to localStorage
                await _localStorage.SetItemAsync("authToken", token);
                await _localStorage.SetItemAsync("userId", responseData.UserId);

                //Add the token to request header of the http client for future requests
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return new UserResult { Succeeded = true, Message = "Successfully logged in user.", Token = token };
            }

            throw new Exception("Token not returned.");
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error: {response.StatusCode} - {errorMessage}");
        }
    }
}

