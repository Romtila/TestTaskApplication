namespace TestTaskApplication.Core.Exceptions;

public class SecureException : Exception
{
    public SecureException(string? message = null) : base(message ?? "Secure exception")
    {
    }
}