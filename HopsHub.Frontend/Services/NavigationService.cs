﻿using System;
using HopsHub.Frontend.Services.Interfaces;
using Microsoft.AspNetCore.Components;
namespace HopsHub.Frontend.Services; 

public class NavigationService : INavigationService
{

	private readonly NavigationManager _navigationManager;

    public bool HomePageActive { get; private set; }

    public bool RatingsPageActive { get; private set; }

    public bool LoginPageActive { get; private set; }

    public bool SettingsPageActive { get; private set; }

    public int ActiveTypeId { get; private set; }

    public NavigationService(NavigationManager navigationManager)
	{
		_navigationManager = navigationManager;
	}

	public string CurrentUri => _navigationManager.Uri.ToLower();

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

    public void NavigateToType(int typeId, string typeName)
    {
        var encodedTypeName = Uri.EscapeDataString(typeName);

        var currentUri = CurrentUri; // NavigationManager.Uri.ToLower();
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
    }

    public void SetActiveTypeId(int typeId)
    {
        ActiveTypeId = typeId;
    }
}

