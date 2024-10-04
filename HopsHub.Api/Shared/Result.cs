using System;
namespace HopsHub.Api.Shared;

public class Result<T>
{
    public bool Succes { get; private set; }

    public T Data { get; private set; }

    public int ErrorCode { get; private set; } 

    public string ErrorMessage { get; private set; } = "";

    public Result(bool success, T data, int errorCode, string errorMessage)
    {
        Succes = success;
        Data = data;
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }
}
