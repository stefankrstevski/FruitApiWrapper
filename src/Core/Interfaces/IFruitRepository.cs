namespace Core.Interfaces;

public interface IFruitRepository
{
    Task<Dictionary<string, string>> GetMetadataAsync(string name);
    Task<bool> AddOrUpdateMetadataAsync(string name, Dictionary<string, string> metadata);
    Task<bool> RemoveMetadataAsync(string name);
}
