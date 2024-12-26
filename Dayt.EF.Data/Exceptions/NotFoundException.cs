namespace Dayt.EF.Data.Exceptions;

public class NotFoundException : Exception
{
    /// <summary>
    /// Not Found Exception; Domain level exception
    /// </summary>
    /// <param name="message"></param>
    public NotFoundException(string? message) : base(message)
    {
    }
}