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
        var idCheck = _context.Animals.Any(a => a.Id == Id);
        if (idCheck == false)
        {
            return NotFound();
        }

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
            ClassificationName = selectedAnimal.Species?.Classification?.Name,
            SpeciesName = selectedAnimal.Species?.Name,
            EnclosureName = selectedAnimal.Enclosure?.Name,
        };

        return selectedAnimalDetails;
    }

    [HttpPost, Route("/AddAnimal/")]
    public IActionResult AddAnimal(
        string name,
        int speciesId,
        DateOnly Birthday,
        DateOnly AcquiredDate,
        int enclosureId
    )
    {
        if (
            !_context.species.Any(a => a.Id == speciesId)
            || !_context.Enclosures.Any(b => b.Id == enclosureId)
        )
        {
            return ValidationProblem("Species and/or Enclosure not found.");
        }
        else
        {
            if (name == null || Birthday == DateOnly.MinValue || AcquiredDate == DateOnly.MinValue)
            {
                return ValidationProblem("Name/Date of Birth/Date of Acquistion is/are null.");
            }
        }
        if (Birthday > AcquiredDate)
        {
            return ValidationProblem("Acquired Date is before Birth Date");
        }
        if (Birthday>DateOnly.FromDateTime(DateTime.Now) || AcquiredDate > DateOnly.FromDateTime(DateTime.Now))
        {
            return ValidationProblem("Birthday or Acquired Date is in the future.");
        }
        _context.Animals.Add(
            new Animal
            {
                Name = name,
                SpeciesId = speciesId,
                DateofAcquisition = AcquiredDate,
                DateOfBirth = Birthday,
                EnclosureId = enclosureId,
            }
        );
        _context.SaveChanges();
        return Accepted();
    }

    [HttpGet, Route("/AnimalList")]
    public async Task<List<animalTypes?>> ListAnimalTypes()
    {
        var AnimalList = await _context
            .Animals.Include(a => a.Species)
            .ThenInclude(s => s.Classification)
            .ToListAsync();

        var selectedAnimalTypes = AnimalList
            .GroupBy(p => p.SpeciesId)
            .Select(g =>
            {
                var firstAnimal = g.FirstOrDefault();
                if (firstAnimal == null)
                    return null;
                return new animalTypes
                {
                    ClassificationName = firstAnimal.Species?.Classification?.Name,
                    SpeciesName = firstAnimal.Species?.Name,
                };
            })
            .Where(x => x != null)
            .ToList();

        return selectedAnimalTypes;
    }
}
