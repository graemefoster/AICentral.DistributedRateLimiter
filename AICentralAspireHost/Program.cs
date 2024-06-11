var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");

builder.AddProject<Projects.AICentral_DistributedRateLimiterTestWeb>("aicentral")
    .WithReference(redis, "")
    .WithEnvironment("AICentral__GenericSteps__0__Properties__RedisConfiguration", redis)
    .WithEnvironment("AICentral__GenericSteps__1__Properties__RedisConfiguration", redis)
    ;

builder.Build().Run();
