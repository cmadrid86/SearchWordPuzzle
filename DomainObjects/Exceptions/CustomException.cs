using System.Runtime.Serialization;

namespace DomainObjects.Exceptions;

[Serializable]
public class CustomException : Exception
{
    public int StatusCode { get; protected set; }
    public string Title { get; protected set; }

    protected CustomException(string message, int statusCode, string title)
        : this(message, statusCode, title, null)
    {
    }

    protected CustomException(string message, int statusCode, string title, Exception? innerException)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        Title = title;
    }

    protected CustomException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Title = nameof(CustomException);
        StatusCode = 500;
    }
}