var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

var app = builder.Build();

var logger = app.Logger;

app.MapGet("/startup", () => {
    logger.LogInformation("Startup endpoint called");
    return Results.Ok(new { status = "started" });
});

app.MapGet("/ready", () => {
    logger.LogInformation("Readiness endpoint called");
    return Results.Ok(new { status = "ready" });
});

app.MapGet("/health", () => {
    logger.LogInformation("Heath endpoint called");
    return Results.Ok(new { status = "healthy" });
});

app.MapGet("/message", () => {
    logger.LogCritical ("1 - Generating Critical Log");
    logger.LogDebug("2 - Generating Debug Log");
    logger.LogError("3 - Generating Error Log");
    logger.LogInformation("4 - Generating Information Log");
    logger.LogTrace("5 - Generating Trace Log");
    logger.LogWarning ("6 - Generating Warning  Log");

    logger.LogInformation("Message endpoint called");
    return Results.Ok("hello world");
});

app.MapGet("/call-other", async (IHttpClientFactory httpClientFactory, IConfiguration config) => {
    var baseUrl = config["OTHER_INSTANCE_BASE_URL"] ?? "http://localhost:8080";
    var targetUrl = $"{baseUrl.TrimEnd('/')}/message";

    logger.LogInformation("Calling other instance at {Url}", targetUrl);

    var client = httpClientFactory.CreateClient();
    var response = await client.GetAsync(targetUrl);
    var body = await response.Content.ReadAsStringAsync();


    logger.LogInformation("Response Status: {Status}", (int)response.StatusCode);

    return Results.Ok(new {
        status = "success",
        target = targetUrl,
        statusCode = (int)response.StatusCode,
        body
    });
});

app.MapPost("/submit", (MessagePayload payload) => {
    logger.LogInformation("Submit endpoint called with payload: {Text}", payload.Text);
    return Results.Ok(new { status = "success", received = payload.Text ?? string.Empty });
});

app.Run();

record MessagePayload(string Text);
