using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class Animal
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public required int SpeciesId { get; set; }

    public Species? Species { get; set; }

    public required DateOnly DateOfBirth { get; set; }
    public required DateOnly DateofAcquisition { get; set; }
    public required int? EnclosureId { get; set; }
    public Enclosure? Enclosure { get; set; }

    internal object Include(Func<object, object> value)
    {
        throw new NotImplementedException();
    }
}

public class animalDTO
{
    public string? Name { get; set; }

    public required DateOnly DateOfBirth { get; set; }
    public required DateOnly DateofAcquisition { get; set; }

    public string? ClassificationName { get; set; }
    public string? SpeciesName { get; set; }

    public string? EnclosureName { get; set; }
}

public class animalTypes
{
    public string? SpeciesName { get; set; }

    public string? ClassificationName { get; set; }
}
