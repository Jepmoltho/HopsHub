using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HopsHub.Frontend.Services.Interfaces;

public interface IAuthenticationStateService
{
    event Action? OnChange;

    bool IsLoggedIn { get; }

    Task InitializeAsync();

    Task LoginAsync(string token, Guid userId);

    Task LogoutAsync();
}

