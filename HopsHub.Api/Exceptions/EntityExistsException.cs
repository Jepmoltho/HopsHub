namespace HopsHub.Api.Exceptions;

public class EntityExistsException : Exception
{
	public EntityExistsException(string message) : base(message)
	{
	}
}

