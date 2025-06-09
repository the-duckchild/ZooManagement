using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace Zoo.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimalController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<AnimalController> _logger;
    private readonly SqliteConnection _Connection;

    public AnimalController(ILogger<AnimalController> logger, SqliteConnection connection)
    {
        _logger = logger;
        _Connection = connection;
    }

    [HttpGet(Name = "GetAnimalById")]
    public IEnumerable<Animal> Get([FromRoute] int urlId)
    {        

        return Enumerable.Range(1, 5).Select(index => new Animal
        {
            Name =
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
    
}
