var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Preserve the property names as they are defined in C# (PascalCase)
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Register the message publisher
builder.Services.AddScoped<Disbursement.Api.Messaging.IDisbursementMessagePublisher, Disbursement.Api.Messaging.ConsoleDisbursementMessagePublisher>();

// Optional: Add Swagger if you want API docs and testing UI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
