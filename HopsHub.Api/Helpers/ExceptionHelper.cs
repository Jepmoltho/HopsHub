namespace HopsHub.Api.Helpers;

public static class ExceptionHelper
{
    public static string PrintMessage(string exception, string? innerException = "")
    {
        return $"Exception: {exception} \n\nInnerException: {innerException}";
    }
}

