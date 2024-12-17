using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace HopsHub.Frontend.Services;

public class AuthenticationStateService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public event Action? OnChange;

    public bool IsLoggedIn { get; private set; }

    public AuthenticationStateService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    // Initialize login state by checking for an auth token
    public async Task InitializeAsync()
    {
        var authToken = await _localStorage.GetItemAsync<string>("authToken");
        if (!string.IsNullOrEmpty(authToken))
        {
            SetHttpHeader(authToken);
            IsLoggedIn = true;
        }
        else
        {
            IsLoggedIn = false;
        }

        NotifyStateChanged();
    }

    // Perform login: set token, HTTP headers, and notify
    public async Task LoginAsync(string token, Guid userId)
    {
        await _localStorage.SetItemAsync("authToken", token);
        await _localStorage.SetItemAsync("userId", userId);
        SetHttpHeader(token);
        IsLoggedIn = true;

        NotifyStateChanged();
    }

    // Perform logout: clear local storage, headers, and notify
    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
        _httpClient.DefaultRequestHeaders.Authorization = null; // Remove auth header
        IsLoggedIn = false;

        NotifyStateChanged();
    }

    // Helper to set the HTTP header
    private void SetHttpHeader(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
