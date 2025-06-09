using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace Zoo.Controllers;

[ApiController]
[Route("[Controller]")]
public class AnimalController : ControllerBase
{
    private readonly ILogger<AnimalController> _logger;
    private readonly ZooDBContext _context;

    public AnimalController(ILogger<AnimalController> logger, ZooDBContext zoodbcontext)
    {
        _logger = logger;
        _context = zoodbcontext;
    }

    [HttpGet, Route("GetAnimalById")]
    public IEnumerable<Animal> AnimalById([FromRoute] int urlId)
    {
        return _context.Animals.Where(b => b.Id == urlId).ToList();
    }
}
