namespace Dayt.EF.Data.Exceptions;

public class ValidationException : Exception
{
    /// <summary>
    /// Validation Exception with custom message; Domain level exception
    /// </summary>
    /// <param name="message"></param>
    public ValidationException(string message) : base(message)
    {
    }

    /// <summary>
    /// Validation Exception with custom message and inner exception; Domain level exception
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public ValidationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}