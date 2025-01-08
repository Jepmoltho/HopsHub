using System;
using System.Web;
using HopsHub.Frontend.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace HopsHub.Frontend.Services; 

public class NavigationService : INavigationService
{

	private readonly NavigationManager _navigationManager;

    public event Action? OnChange;

    public bool HomePageActive { get; private set; }

    public bool RatingsPageActive { get; private set; }

    public bool LoginPageActive { get; private set; }

    public bool SettingsPageActive { get; private set; }

    public int ActiveTypeId { get; private set; }

    public NavigationService(NavigationManager navigationManager)
	{
		_navigationManager = navigationManager;
        _navigationManager.LocationChanged += HandleLocationChanged;
    }

    public string CurrentUri => _navigationManager.Uri.ToLower();

    private void HandleLocationChanged(object? sender, LocationChangedEventArgs args)
    {
        SetActivePage();
    }

    public string GetRelativeUri()
	{
		var baseUri = new Uri(CurrentUri);

		return baseUri.AbsolutePath.TrimStart('/');
	}

	public bool IsOnPageSegment(string pageSegment)
	{
		return CurrentUri.ToLower().Contains(pageSegment);
	}

    public bool IsOnHomePage()
    {
        if (!IsOnPageSegment("/ratings") &&
            !IsOnPageSegment("/settings") &&
            !IsOnPageSegment("/login"))
        {
            return true;
        }

        return false;
    }

    public void NavigateTo(string segment)
    {
        _navigationManager.NavigateTo(segment);
    }

    public void NavigateToType(int typeId, string typeName)
    {
        var encodedTypeName = Uri.EscapeDataString(typeName);

        var currentUri = CurrentUri;
        var isFromRatingsPage = currentUri.Contains("/ratings");

        var baseRoute = isFromRatingsPage ? "/ratings" : "";
        var targetRoute = typeId == 0 ? baseRoute : $"{baseRoute}/{encodedTypeName}?typeId={typeId}";

        _navigationManager.NavigateTo(targetRoute);

        ActiveTypeId = typeId;
    }

    public string GetTargetUriExtention()
    {
        var uri = CurrentUri;

        var baseUri = new Uri(uri);
        var relativeUri = baseUri.AbsolutePath.TrimStart('/');
        var query = baseUri.Query;

        if (relativeUri.Contains("ratings"))
        {
            relativeUri = relativeUri.Replace("ratings", "");
        }

        if (relativeUri.Contains("settings"))
        {
            relativeUri = relativeUri.Replace("settings", "");
        }

        if (relativeUri.Contains("login"))
        {
            relativeUri = relativeUri.Replace("login", "");
        }

        var uriExtension = relativeUri + query;

        return uriExtension.StartsWith("/") ? uriExtension : $"/{uriExtension}";
    }

    public void SetActivePage()
    {
        var uri = CurrentUri;

        if (uri.Contains("ratings"))
        {
            HomePageActive = false;
            RatingsPageActive = true;
            LoginPageActive = false;
            SettingsPageActive = false;
        }
        else if (uri.Contains("login"))
        {
            HomePageActive = false;
            RatingsPageActive = false;
            LoginPageActive = true;
            SettingsPageActive = false;
        }
        else if (uri.Contains("settings"))
        {
            HomePageActive = false;
            RatingsPageActive = false;
            LoginPageActive = false;
            SettingsPageActive = true;
        }
        else
        {
            HomePageActive = true;
            RatingsPageActive = false;
            LoginPageActive = false;
            SettingsPageActive = false;
        }

        //You are invoking the onChange method telling the subscribers (NavMenu), to rerender and call StateHasChanges
        OnChange?.Invoke();
    }

    public void SetActiveTypeId(int typeId)
    {
        ActiveTypeId = typeId;
    }

    public int GetActiveTypeId()
    {
        var uri = _navigationManager.ToAbsoluteUri(CurrentUri);

        var queryParams = HttpUtility.ParseQueryString(uri.Query);

        if (!string.IsNullOrEmpty(queryParams["typeId"]))
        {
            return int.TryParse(queryParams["typeId"], out var parsedId) ? parsedId : 0;
        }
        else
        {
            return 0;
        }
    }

    //HERE
    public void Refresh()
    {
        _navigationManager.Refresh();
    }
}

