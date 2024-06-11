using AICentral;
using AICentral.Configuration;
using AICentral.DistributedTokenLimits;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAICentral(
    builder.Configuration,
    additionalComponentAssemblies:
    [
        typeof(DistributedRateLimiter).Assembly,
    ]);

builder.Services
    .AddOpenTelemetry()
    .WithMetrics(metrics => { metrics.AddMeter(ActivitySource.AICentralTelemetryName); });

var useOtlpExporter =
    !string.IsNullOrWhiteSpace(
        builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

if (useOtlpExporter)
{
    builder.Services.Configure<OpenTelemetryLoggerOptions>(
        logging => logging.AddOtlpExporter());
    builder.Services.ConfigureOpenTelemetryMeterProvider(
        metrics => metrics.AddOtlpExporter());
    builder.Services.ConfigureOpenTelemetryTracerProvider(
        tracing => tracing.AddOtlpExporter());
}

var app = builder.Build();

app.UseAICentral();

app.Run();