using Azure.Monitor.OpenTelemetry.Exporter;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetValue<string>("ApplicationInsights:ConnectionString");

Action<ResourceBuilder> configureResource = r => r.AddService(
    serviceName: "Sample",
    serviceVersion: typeof(Program).Assembly.GetName().Version?.ToString() ?? "unknown",
    serviceInstanceId: Environment.MachineName);


builder.Logging.ClearProviders();

// Configure OpenTelemetry Logging.
builder.Logging.AddOpenTelemetry(options =>
{
    var resourceBuilder = ResourceBuilder.CreateDefault();
    configureResource(resourceBuilder);
    options.IncludeFormattedMessage = true;
    options.IncludeScopes = true;
    options.SetResourceBuilder(resourceBuilder);
    options.AddAzureMonitorLogExporter(options => options.ConnectionString = connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/sample-endpoint", (ILogger<Program> logger) =>
{
    logger.LogInformation("Sample log message");

    return Results.NoContent();
});

app.Run();
