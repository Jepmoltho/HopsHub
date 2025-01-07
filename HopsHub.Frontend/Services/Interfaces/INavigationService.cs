using System;
namespace HopsHub.Frontend.Services.Interfaces;

public interface INavigationService
{
    string CurrentUri { get; }

    bool HomePageActive { get; }

    bool RatingsPageActive { get; }

    bool LoginPageActive { get;  }

    bool SettingsPageActive { get; }

    string GetTargetUriExtention();

    string GetRelativeUri();

    bool IsOnPageSegment(string pageSegment);

    bool IsOnHomePage();

    void SetActivePage();
}

