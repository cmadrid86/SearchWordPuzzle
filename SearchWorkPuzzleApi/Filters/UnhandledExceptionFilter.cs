using Microsoft.AspNetCore.Mvc.Filters;

namespace SearchWorkPuzzleApi.Filters;

internal class UnhandledExceptionFilter : IExceptionFilter
{
    private readonly ILogger<UnhandledExceptionFilter> _logger;

    public UnhandledExceptionFilter(ILogger<UnhandledExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "ID: {Id} Message: {Message}", context.HttpContext.TraceIdentifier, context.Exception.Message);
    }
}
