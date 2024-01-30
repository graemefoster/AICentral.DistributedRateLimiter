using AICentral.Configuration;
using AICentral.DistributedTokenLimits;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAICentral(
    builder.Configuration,
    additionalComponentAssemblies:
    [
        typeof(DistributedRateLimiter).Assembly,
    ]);

var app = builder.Build();

app.UseAICentral();

app.Run();