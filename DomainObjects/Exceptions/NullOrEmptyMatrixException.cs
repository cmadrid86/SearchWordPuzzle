namespace DomainObjects.Exceptions;

[Serializable]
public class NullOrEmptyMatrixException : CustomException
{
    [NonSerialized]
    public const string DefaultTitle = nameof(NullOrEmptyMatrixException);

    [NonSerialized]
    public const string DefaultMessage = "The matrix cannot be null or empty";

    [NonSerialized]
    public const int DefaultStatusCode = 401;

    public NullOrEmptyMatrixException()
        : base(DefaultMessage, DefaultStatusCode, DefaultTitle)
    {
    }

    protected NullOrEmptyMatrixException(string message, int statusCode, string title) : base(message, statusCode, title)
    {
    }
}