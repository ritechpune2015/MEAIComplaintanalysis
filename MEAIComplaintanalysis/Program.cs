using MEAIComplaintanalysis.Services;
using Microsoft.Extensions.AI;
using OpenAI;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------
// Configuration
// --------------------------------------------------

string apiKey =
    builder.Configuration["OpenAIKey"]
    ?? throw new InvalidOperationException(
        "OpenAIKey is not configured.");

string modelName =
    builder.Configuration["ModelName"]
    ?? throw new InvalidOperationException(
        "ModelName is not configured.");


// --------------------------------------------------
// Register IChatClient
// --------------------------------------------------

IChatClient chatClient =
    new OpenAIClient(apiKey)
        .GetChatClient(modelName)
        .AsIChatClient();

builder.Services.AddSingleton(chatClient);


// --------------------------------------------------
// Application Services
// --------------------------------------------------

builder.Services.AddScoped<IComplaintAnalysisService,ComplaintAnalysisService>();


// --------------------------------------------------
// ASP.NET Core Services
// --------------------------------------------------

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// --------------------------------------------------
// Middleware
// --------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

