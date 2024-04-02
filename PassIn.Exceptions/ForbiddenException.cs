namespace PassIn.Exceptions;

public class ForbiddenException : PassInException
{
    public ForbiddenException(string message) : base(message)
    {
    }
}
