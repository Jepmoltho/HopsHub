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

    bool TypeBarIsActive { get; }

    string GetTargetUriExtention();

    string GetRelativeUri();

    bool IsOnPageSegment(string pageSegment);

    bool IsOnHomePage();

    void SetActivePage();

    void SetActiveTypeId(int typeId);

    int GetActiveTypeId();

    void NavigateTo(string segment);

    void NavigateToType(int typeId, string typeName);

    public void SetTypeBarState();
}

