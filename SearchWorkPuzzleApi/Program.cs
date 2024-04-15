using Hellang.Middleware.ProblemDetails;
using SearchWorkPuzzleApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services and custom configurations to the container.
builder.Services.ConfigureMvcOptions();
builder.Services.ConfigureProblemDetails();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseProblemDetails();

app.MapControllers();

app.Run();
