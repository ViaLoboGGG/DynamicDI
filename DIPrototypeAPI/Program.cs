using DIPrototype;
using DIPrototype.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
ConfigSettings.FileUploadLocation = builder.Configuration["Settings:FileUploadLocation"];
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//This works because of dotnet shortcircuiting if the first doesnt eval to true the second will never even be evaluated
//var test = builder.Configuration.GetSection("FeatureFlags")["NewTransactionQue"];

if (builder.Configuration.GetSection("FeatureFlags")["NewTransactionQue"] != null && bool.Parse(builder.Configuration.GetSection("FeatureFlags")["NewTransactionQue"]))
{
    builder.Services.AddScoped<ITransactionQueService, TransactionQueService>();
} else builder.Services.AddScoped<ITransactionQueService, TransactionQueService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
