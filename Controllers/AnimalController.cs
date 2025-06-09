using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
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

    [HttpGet, Route("/{Id}")]
    public async Task<ActionResult<animalDTO>> AnimalById(int Id)
    {
        var selectedAnimal = await _context
            .Animals.Include(a => a.Species)
            .ThenInclude(s => s.Classification)
            .Include(a => a.Enclosure)
            .SingleAsync(a => a.Id == Id);

        var selectedAnimalDetails = new animalDTO()
        {
            Name = selectedAnimal.Name,
            DateOfBirth = selectedAnimal.DateOfBirth,
            DateofAcquisition = selectedAnimal.DateofAcquisition,
            ClassificationName = selectedAnimal.Species.Classification.Name,
            SpeciesName = selectedAnimal.Species.Name,
            EnclosureName = selectedAnimal.Enclosure.Name,
        };

        if (selectedAnimal == null)
        {
            return NotFound();
        }
        else
        {
            return selectedAnimalDetails;
        }
    }
}

public interface IHttpActionResult { }
