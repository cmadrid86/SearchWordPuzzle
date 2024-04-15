namespace DomainObjects.Exceptions;

[Serializable]
public class NotSameLengthRowException : CustomException
{
    [NonSerialized]
    public const string DefaultTitle = nameof(NotSameLengthRowException);

    [NonSerialized]
    public const string DefaultMessage = "A row in the matrix does not have the same length as other";

    [NonSerialized]
    public const int DefaultStatusCode = 401;

    public NotSameLengthRowException()
        : base(DefaultMessage, DefaultStatusCode, DefaultTitle)
    {
    }

    protected NotSameLengthRowException(string message, int statusCode, string title) : base(message, statusCode, title)
    {
    }
}