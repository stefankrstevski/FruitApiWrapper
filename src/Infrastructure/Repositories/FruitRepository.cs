using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FruitRepository : IFruitRepository
{
    private readonly ApplicationDbContext _context;

    public FruitRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Dictionary<string, string>> GetMetadataAsync(string name)
    {
        return await _context.FruitMetadata
            .Where(m => m.FruitName == name)
            .ToDictionaryAsync(m => m.Key, m => m.Value);
    }

    public async Task<bool> AddOrUpdateMetadataAsync(string name, Dictionary<string, string> metadata)
    {
        var existingMetadataList = await _context.FruitMetadata
            .Where(m => m.FruitName == name)
            .ToListAsync();

        var metadataEntriesToRemove = new List<FruitMetadata>();

        foreach (var existingMetadata in existingMetadataList)
        {
            if (metadata.TryGetValue(existingMetadata.Key, out var newValue))
            {
                existingMetadata.Value = newValue;
                metadata.Remove(existingMetadata.Key);
            }
            else
            {
                metadataEntriesToRemove.Add(existingMetadata);
            }
        }

        if (metadataEntriesToRemove.Any())
        {
            _context.FruitMetadata.RemoveRange(metadataEntriesToRemove);
        }

        if (metadata.Any())
        {
            var metadataEntriesToInsert = metadata.Select(m => new FruitMetadata
            {
                FruitName = name,
                Key = m.Key,
                Value = m.Value
            }).ToList();

            _context.FruitMetadata.AddRange(metadataEntriesToInsert);
        }

        var recordsAffected = await _context.SaveChangesAsync();
        return recordsAffected > 0;
    }

    public async Task<bool> RemoveMetadataAsync(string name)
    {
        var metadataList = await _context.FruitMetadata
            .Where(m => m.FruitName == name)
            .ToListAsync();

        if (metadataList.Any())
        {
            _context.FruitMetadata.RemoveRange(metadataList);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}
