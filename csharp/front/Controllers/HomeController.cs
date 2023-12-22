using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using front.Models;

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace front.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        
        string action = "test";
        var val = "No content";
        return View( new RepViewModel { Value = val });
    }

    public IActionResult Test(string test)
    {
        
        var val = "No content";
        if(test == "test")
        {
            val = "test";
        }
        else{
            // Console.WriteLine($"Querying {test}");
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"http://localhost:5022/{test}";
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
        
        }
        return View( new RepViewModel { Value = val });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
