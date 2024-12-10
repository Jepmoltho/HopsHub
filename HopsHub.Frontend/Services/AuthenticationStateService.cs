using System;
using HopsHub.Frontend.Services.Interfaces;
namespace HopsHub.Frontend.Services;

public class AuthenticationStateService
{
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

