namespace Account.Application.Exceptions;

public class InsufficientBalanceException : ApplicationException
{
    public InsufficientBalanceException(string message) : base(message)
    {
    }
}