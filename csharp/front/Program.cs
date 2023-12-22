using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddOpenTelemetry()
    .ConfigureResource(r => r.AddService("frontend"))
    // .AddHttpInstrumentation()
        
    .WithTracing(builder => builder
        //   .AddHttpClientInstrumentation()
          .AddZipkinExporter()
        .AddAspNetCoreInstrumentation()
        // .AddConsoleExporter()
        )
    .WithMetrics(builder => builder
            .AddHttpClientInstrumentation()
            .AddAspNetCoreInstrumentation()
            // .AddConsoleExporter()
            );
            


    // .AddOpenTelemetryInstrumentationHttp()
    // .AddOpenTelemetrySamplerAlwaysOn();
// builder.Services.A
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
