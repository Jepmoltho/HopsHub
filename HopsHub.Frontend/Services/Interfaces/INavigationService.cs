using System;
using Microsoft.AspNetCore.Components;

namespace HopsHub.Frontend.Services.Interfaces;

public interface INavigationService
{
    string CurrentUri { get; }

    public event Action? OnChange;

    bool HomePageActive { get; }

    bool RatingsPageActive { get; }

    bool LoginPageActive { get;  }

    bool SettingsPageActive { get; }

    int ActiveTypeId { get; }

    string GetTargetUriExtention();

    string GetRelativeUri();

    bool IsOnPageSegment(string pageSegment);

    bool IsOnHomePage();

    void SetActivePage();

    void SetActiveTypeId(int typeId);

    void NavigateToType(int typeId, string typeName);
}

