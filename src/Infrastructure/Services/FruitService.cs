using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using System.Text.Json;

namespace Infrastructure.Services;

public class FruitService : IFruitService
{
    private readonly HttpClient _httpClient;
    private readonly IFruitCacheService _cacheService;
    private readonly IFruitRepository _fruitRepository;

    public FruitService(HttpClient httpClient, IFruitCacheService cacheService, IFruitRepository fruitRepository)
    {
        _httpClient = httpClient;
        _cacheService = cacheService;
        _fruitRepository = fruitRepository;
    }

    public async Task<FruitWithMetadata> GetFruitWithMetadataAsync(string name)
    {
        var fruit = await _cacheService.GetCachedFruitAsync(name);
        if (fruit == null)
        {
            fruit = await FetchFruitFromApiAsync(name);
            await _cacheService.SetCachedFruitAsync(name, fruit);
        }

        var metadata = await _fruitRepository.GetMetadataAsync(name);

        return new FruitWithMetadata
        {
            Name = fruit.Name,
            Family = fruit.Family,
            Genus = fruit.Genus,
            Order = fruit.Order,
            Nutritions = fruit.Nutritions,
            Metadata = metadata
        };
    }

    public async Task<bool> AddOrUpdateMetadataAsync(string name, Dictionary<string, string> metadata)
    {
        await FetchFruitFromApiAsync(name);
        return await _fruitRepository.AddOrUpdateMetadataAsync(name, metadata);
    }

    public async Task<bool> RemoveMetadataAsync(string name)
    {
        await FetchFruitFromApiAsync(name);
        return await _fruitRepository.RemoveMetadataAsync(name);
    }

    private async Task<Fruit> FetchFruitFromApiAsync(string name)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{name}");

            if (!response.IsSuccessStatusCode)
            {
                throw new FruitNotFoundException(name);
            }

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Fruit>(responseData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Issue reaching the FruityviceApi.", ex);
        }
        catch (JsonException ex)
        {
            throw new Exception("Problem deserializing JSON.", ex);
        }
    }
}
