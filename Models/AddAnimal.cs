public class AddAnimal
{
    public string? Name { get; set; }
    public required int SpeciesId { get; set; }
    public required DateOnly DateOfBirth { get; set; }
    public required DateOnly DateofAcquisition { get; set; }
    public required int? EnclosureId { get; set; }
}
