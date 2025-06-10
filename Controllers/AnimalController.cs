using Bogus;
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
        if (
            Birthday > DateOnly.FromDateTime(DateTime.Now)
            || AcquiredDate > DateOnly.FromDateTime(DateTime.Now)
        )
        {
            return ValidationProblem("Birthday or Acquired Date is in the future.");
        }
        /*
        Enclosure enclosure = _context.Enclosures.Find(enclosureId);
        if (enclosure != null && enclosure.Capacity)
        */
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

    // public void GenerateSeedData()
    // {
    //     int largestKey = _context.Set<Animal>().Max(e => e.Id);

    //     var animalId = largestKey + 1;
    //     var animalFaker = new Faker<Animal>()
    //         .RuleFor(a => a.Id, _ => animalId++)
    //         .RuleFor(a => a.Name, f => f.Name.FirstName())
    //         .RuleFor(a => a.SpeciesId, f => f.Random.Number(1, 9))
    //         .RuleFor(
    //             a => a.DateofAcquisition,
    //             f =>
    //                 DateOnly.FromDateTime(
    //                     f.Date.Between(new DateTime(2024, 1, 1), new DateTime(2025, 1, 1))
    //                 )
    //         )
    //         .RuleFor(
    //             a => a.DateOfBirth,
    //             f =>
    //                 DateOnly.FromDateTime(
    //                     f.Date.Between(new DateTime(2020, 1, 1), new DateTime(2023, 1, 1))
    //                 )
    //         )
    //         .RuleFor(a => a.EnclosureId, f => f.Random.Number(1, 5));

    //     var fakeAnimals = animalFaker.Generate(100);
    //     _context.Add(fakeAnimals);
    //     _context.SaveChanges();
    // }

}
