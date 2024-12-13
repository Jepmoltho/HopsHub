﻿using System;
using HopsHub.Frontend.Services.Interfaces;
using System.Net.Http;
using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace HopsHub.Frontend.Services;

public class AuthenticationStateService
{
	//private readonly HttpClient _httpClient;
	//private readonly ILocalStorageService _localStorage;

	//public AuthenticationStateService(HttpClient httpClient, ILocalStorageService localStorage)
	//{
	//	_httpClient = httpClient;
	//	_localStorage = localStorage;
	//}

   // public async Task InitializeAsync()
   // {
   //     var token = await _localStorage.GetItemAsync<string>("authToken");

   //     if (!string.IsNullOrEmpty(token))
   //     {
			//_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
   //         //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
   //     }
   // }


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
}

