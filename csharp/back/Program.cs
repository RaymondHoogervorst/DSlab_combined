using System;
using System.Net.Http;
using System.Numerics;
using System.Threading.Tasks;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

// var builder = WebApplication.CreateBuilder(args);
// builder.Logging.ClearProviders();


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.AddConsole();
        builder.Services.AddOpenTelemetry()
            .ConfigureResource(r => r.AddService("back-end"))
            .WithTracing(builder => builder
                //   .AddHttpClientInstrumentation()
                .AddZipkinExporter()
                  .AddAspNetCoreInstrumentation()
                .AddConsoleExporter())
            .WithMetrics(builder => builder
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddConsoleExporter()); ;

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

        app.MapGet("/pi", (int n = 100) =>
        {
            // return Digi
            var retList = new List<int>();
            // var n = 10;
            foreach (var d in GenDigits().Take(n))
                {
                    Console.WriteLine(d);
                    retList.Add(d);}
                Console.WriteLine(retList.Count);
            return retList.ToArray();
        })
        .WithName("CalcPi")
        .WithOpenApi();

        app.MapGet("/fac", (int n = 5) =>
        {
            // return Digi
            var retList = new List<int>();
            // var n = 10;
            return CalculateFactorial(n);
        })
        .WithName("Fac")
        .WithOpenApi();


        app.MapGet("/matrix", (int n = 100) =>
        {
            MemoryAndCPUIntensiveLoad(n);
            return "Matrix";
        })
        .WithName("MatrixCalc")
        .WithOpenApi();


        app.MapGet("/small", (int n = 100) =>
        {
            // MemoryAndCPUIntensiveLoad(n);
            return "Small";
        })
        .WithName("Small")
        .WithOpenApi();



        app.MapGet("/large", (int n = 4000) =>
        {
            
            return Enumerable.Range(1, n).ToList();;
        })
        .WithName("Large")
        .WithOpenApi();



        app.Run();
    }


static long CalculateFactorial(int n)
{
    if (n == 0 || n == 1)
        return 1;
    return n * CalculateFactorial(n - 1);
}

 static void MemoryAndCPUIntensiveLoad(int size)
    {
        Console.WriteLine($"Introducing memory and CPU-intensive load with matrix multiplication ({size}x{size})!");

        // Initialize matrices
        int[,] matrixA = InitializeMatrix(size);
        int[,] matrixB = InitializeMatrix(size);
        int[,] resultMatrix = new int[size, size];

        // Perform matrix multiplication
        MultiplyMatrices(matrixA, matrixB, resultMatrix);

        Console.WriteLine($"Memory and CPU-intensive load complete!");
    }

    static int[,] InitializeMatrix(int size)
    {
        int[,] matrix = new int[size, size];

        // Initialize matrix with random values
        Random random = new Random();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = random.Next(1, 10);
            }
        }

        return matrix;
    }

    static void MultiplyMatrices(int[,] matrixA, int[,] matrixB, int[,] resultMatrix)
    {
        int size = matrixA.GetLength(0);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    resultMatrix[i, j] += matrixA[i, k] * matrixB[k, j];
                }
            }
        }
    }

static IEnumerable<int> GenDigits()
{
    var k = 1;
    var n1 = new BigInteger(4);
    var n2 = new BigInteger(3);
    var d = BigInteger.One;
    var u = new BigInteger();
    var v = new BigInteger();
    var w = BigInteger.Zero;

    while (true)
    {
        // digit
        u = BigInteger.Divide(n1, d);
        v = BigInteger.Divide(n2, d);

        if (BigInteger.Compare(u,v) == 0)                
        {
            yield return (int)u;

            // extract
            u = BigInteger.Multiply(u, -10);
            u = BigInteger.Multiply(u, d);
            n1 = BigInteger.Multiply(n1, 10);
            n1 = BigInteger.Add(n1, u);
            n2 = BigInteger.Multiply(n2, 10);
            n2 = BigInteger.Add(n2, u);
        }
        else
        {
            // produce
            var k2 = k * 2;
            u = BigInteger.Multiply(n1, k2 - 1);
            v = BigInteger.Add(n2, n2);
            w = BigInteger.Multiply(n1, k - 1);
            n1 = BigInteger.Add(u, v);
            u = BigInteger.Multiply(n2, k + 2);
            n2 = BigInteger.Add(w, u);
            d = BigInteger.Multiply(d, k2 + 1);
            k++;
        }
    }
}

}


