public class Animal
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public required string Species { get; set; }
    public required string Classification { get; set; }
    public required DateOnly DateOfBirth { get; set; }
    public required DateOnly DateofAcquisition { get; set; }
    public required int EnclosureId { get; set; }
}
