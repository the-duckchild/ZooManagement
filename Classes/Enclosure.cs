public class Enclosure
{
    public int Id { get; set; }
    public required string? Name { get; set; }
    public required int Capacity { get; set; }
    public List<Animal>? Animals { get; set; }
}
