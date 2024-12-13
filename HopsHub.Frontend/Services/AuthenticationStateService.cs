using System;
using HopsHub.Frontend.Services.Interfaces;
using System.Net.Http;
using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace HopsHub.Frontend.Services;

public class AuthenticationStateService
{
	private readonly HttpClient _httpClient;

	public AuthenticationStateService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public event Action? OnChange;
	private bool _isLoggedIn;

	public bool IsLoggedIn
	{
		get => _isLoggedIn;

		private set
		{
			if (_isLoggedIn != value)
			{
				_isLoggedIn = value;
				NotifyStateChanged();
			}
		}
	}

	public void SetLoginState(bool isLoggedIn)
	{
		IsLoggedIn = isLoggedIn;
	}

	private void NotifyStateChanged() => OnChange?.Invoke();

	public void SetHttpHeader(string token)
	{
		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
	}
}

