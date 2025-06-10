using Bogus;

public class SeedData
{
    public static void GenerateSeedData(ZooDBContext context)
    {
        var animalId = 0;
        var animalFaker = new Faker<Animal>()
            .RuleFor(a => a.Id, _ => animalId++)
            .RuleFor(a => a.Name, f => f.Name.FirstName())
            .RuleFor(a => a.SpeciesId, f => f.Random.Number(1, 9))
            .RuleFor(
                a => a.DateofAcquisition,
                f => f.Date.BetweenDateOnly(new DateOnly(2023, 1, 1), new DateOnly(2024, 1, 1))
            )
            .RuleFor(
                a => a.DateOfBirth,
                f => f.Date.BetweenDateOnly(new DateOnly(2020, 1, 1), new DateOnly(2022, 1, 1))
            )
            .RuleFor(a => a.EnclosureId, f => f.Random.Number(1, 5));

        var fakeAnimals = animalFaker.Generate(100);
        context.AddRange(fakeAnimals);
        context.SaveChanges();
    }
}
