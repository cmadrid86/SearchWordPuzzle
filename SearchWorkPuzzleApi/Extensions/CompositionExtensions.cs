using Hellang.Middleware.ProblemDetails.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using SearchWorkPuzzleApi.Filters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SearchWorkPuzzleApi.Extensions;

internal static class CompositionExtensions
{
    public static IServiceCollection ConfigureMvcOptions(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });

        services.AddControllers(config =>
        {
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));

            config.ReturnHttpNotAcceptable = true;

            config.OutputFormatters
                .OfType<SystemTextJsonOutputFormatter>()
                .FirstOrDefault()?
                .SupportedMediaTypes
                .Remove("text/json");

            config.Filters.Add<UnhandledExceptionFilter>();
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        })
        .AddProblemDetailsConventions();

        return services;
    }
}