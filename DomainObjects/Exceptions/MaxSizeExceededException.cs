namespace DomainObjects.Exceptions;

[Serializable]
public class MaxSizeExceededException : CustomException
{
    [NonSerialized]
    public const string DefaultTitle = nameof(MaxSizeExceededException);

    [NonSerialized]
    public const string DefaultMessage = "The matrix size cannot exceed 64 x 64";

    [NonSerialized]
    public const int DefaultStatusCode = 401;

    public MaxSizeExceededException()
        : base(DefaultMessage, DefaultStatusCode, DefaultTitle)
    {
    }

    protected MaxSizeExceededException(string message, int statusCode, string title) : base(message, statusCode, title)
    {
    }
}