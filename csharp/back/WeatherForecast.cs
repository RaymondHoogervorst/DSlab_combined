record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}



        // static void Main(string[] args)
        // {
        // int DigitsPerLine = 10;
        //     var n = int.Parse(args[0]);
        //     var i = 1;

        //     foreach (var d in GenDigits().Take(n))
        //     {
        //         Console.Out.Write(d);
        //         if ((i % DigitsPerLine) == 0)
        //             Console.Out.WriteLine("\t:" + i);
        //         i++;
        //     }

        //     // Pad out any trailing digits for the final line
        //     if ((n % DigitsPerLine) > 0)
        //         Console.Out.WriteLine(new string(' ', (DigitsPerLine - (n % DigitsPerLine))) + "\t:" + n);
        // }

