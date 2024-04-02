namespace PassIn.Exceptions;

public class ValidationErrorException : PassInException
{
    public ValidationErrorException(string message) : base(message)
    {
    }
}
