using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using HopsHub.Frontend.Services.Interfaces;
using Blazored.LocalStorage;

namespace HopsHub.Frontend.Services;

public class AuthenticationStateService : IAuthenticationStateService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public event Action? OnChange;

    public bool IsLoggedIn { get; private set; }

    //Makes sure the authentificationStateService is only initialized once to aboid reduntant api calls on statechange
    public bool IsInitialized { get; private set; }

    public AuthenticationStateService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task InitializeAsync()
    {
        if (IsInitialized) return;
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

        IsInitialized = true;
        NotifyStateChanged();
    }

    public async Task LoginAsync(string token, Guid userId)
    {
        await _localStorage.SetItemAsync("authToken", token);
        await _localStorage.SetItemAsync("userId", userId);
        SetHttpHeader(token);
        IsLoggedIn = true;

        NotifyStateChanged();
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
        await _localStorage.RemoveItemAsync("userId");
        _httpClient.DefaultRequestHeaders.Authorization = null; 
        IsLoggedIn = false;

        NotifyStateChanged();
    }

    private void SetHttpHeader(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
