namespace HopsHub.Api.Shared;

public class Result<T>
{
    public bool Succes { get; private set; }

    public T Data { get; private set; }

    public string ErrorMessage { get; private set; } = "";

    public Result(bool success, T data, string errorMessage)
    {
        Succes = success;
        Data = data;
        ErrorMessage = errorMessage;
    }
}
