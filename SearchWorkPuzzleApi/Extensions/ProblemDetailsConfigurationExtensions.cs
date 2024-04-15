using DomainObjects.Exceptions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace SearchWorkPuzzleApi.Extensions;

internal static class ProblemDetailsConfigurationExtensions
{
    public static IServiceCollection ConfigureProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.IncludeExceptionDetails = (ctx, er) => false;
            options.Map<CustomException>(ex => ex.ToProblemDetails());
        });

        return services;
    }

    private static ProblemDetails ToProblemDetails(this CustomException ex) => new()
    {
        Detail = ex.Message,
        Status = ex.StatusCode,
        Title = ex.Title,
        Type = $"https://httpstatuses.com/{ex.StatusCode}"
    };
}