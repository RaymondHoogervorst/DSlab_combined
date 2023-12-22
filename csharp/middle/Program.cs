using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;




internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Services.AddOpenTelemetry()
            .ConfigureResource(r => r.AddService("middle-end"))
            .WithTracing(builder => builder
                     .AddZipkinExporter()
                //   .AddHttpClientInstrumentation()
                  .AddAspNetCoreInstrumentation()
                .AddConsoleExporter())
            .WithMetrics(builder => builder
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddConsoleExporter()); ;


        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // app.Logger = new LoggerConfiguration()
        //     .MinimumLevel.Debug()
        //     .WriteTo.Console()
        //     .CreateLogger();
        // app.Logger.LogInformation("Testing OpenTelemetry logging.");

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

        app.MapGet("/matrix", () => query_backend("matrix")).WithName("matrix").WithOpenApi();
        app.MapGet("/fac", () => query_backend("fac")).WithName("fac").WithOpenApi();
        app.MapGet("/pi", () => query_backend("pi")).WithName("pi").WithOpenApi();
        app.MapGet("/small", () => query_backend("small")).WithName("small").WithOpenApi();
        app.MapGet("/large", () => query_backend("large")).WithName("large").WithOpenApi();

        app.Run();
    }

    static string query_backend(string action)
    {
        var val = "No content";
        using (HttpClient client = new HttpClient())
        {

            string apiUrl = $"http://localhost:5095/{action}";
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                // Read and print the response content
                string content = response.Content.ReadAsStringAsync().Result;
                val = content;
                // Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                Console.ReadLine();
            }
        }
        return val;


    }
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
