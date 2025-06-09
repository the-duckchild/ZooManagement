public class Species
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public required int ClassificationId { get; set; }

    public Classification? Classification { get; set; }
}
