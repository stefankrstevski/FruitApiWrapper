namespace Core.Entities;

public class FruitWithMetadata
{
    public string Name { get; set; }
    public string Family { get; set; }
    public string Genus { get; set; }
    public string Order { get; set; }
    public Nutritions Nutritions { get; set; }
    public Dictionary<string, string> Metadata { get; set; }
}
