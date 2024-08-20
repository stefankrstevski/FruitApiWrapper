using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Helpers;

namespace Presentation.Api.Controllers;

[ApiController]
[Route("api/fruits")]
public class FruitsController : ControllerBase
{
    private readonly IFruitService _fruitService;

    public FruitsController(IFruitService fruitService)
    {
        _fruitService = fruitService;
    }

    /// <summary>
    /// Retrieves fruit with metadata for a specific fruit by its name.
    /// </summary>
    /// <param name="name">The name of the fruit to retrieve.</param>
    /// <returns>The metadata associated with the fruit.</returns>
    [HttpGet("{name}")]
    public async Task<ActionResult<ApiResponse<FruitWithMetadata>>> GetFruit(string name)
    {
        try
        {
            name = name.ToLower();

            var fruitWithMetadata = await _fruitService.GetFruitWithMetadataAsync(name);
            if (fruitWithMetadata != null)
                return Ok(new ApiResponse<FruitWithMetadata>(fruitWithMetadata, "Fruit fetched successfully."));
            else
                return NotFound(new ApiResponse<FruitWithMetadata>(null, "Fruit not found"));
        }
        catch (FruitNotFoundException ex)
        {
            return NotFound(new ApiResponse<FruitWithMetadata>(ex.Message));
        }
    }

    /// <summary>
    /// Adds or updates metadata for a specific fruit.
    /// </summary>
    /// <param name="name">The name of the fruit.</param>
    /// <param name="metadata">The metadata to add or update.</param>
    /// <returns>A message indicating success or failure.</returns>
    [HttpPost("metadata/{name}")]
    public async Task<ActionResult<ApiResponse<string>>> AddOrUpdateMetadata(string name, [FromBody] Dictionary<string, string> metadata)
    {
        try
        {
            name = name.ToLower();

            var success = await _fruitService.AddOrUpdateMetadataAsync(name, metadata);
            if (success)
                return Ok(new ApiResponse<string>(null, "Metadata added/updated successfully."));
            else
                return BadRequest(new ApiResponse<string>("Failed to add/update metadata."));
        }
        catch (FruitNotFoundException ex)
        {
            return BadRequest(new ApiResponse<string>(ex.Message));
        }
    }

    /// <summary>
    /// Removes metadata for a specific fruit.
    /// </summary>
    /// <param name="name">The name of the fruit whose metadata is to be removed.</param>
    /// <returns>A message indicating success or failure.</returns>
    [HttpDelete("metadata/{name}")]
    public async Task<ActionResult<ApiResponse<string>>> RemoveMetadata(string name)
    {
        try
        {
            name = name.ToLower();

            var success = await _fruitService.RemoveMetadataAsync(name);
            if (success)
                return Ok(new ApiResponse<string>(null, "Metadata removed successfully."));
            else
                return NotFound(new ApiResponse<string>($"Metadata not found for fruit '{name}'."));
        }
        catch (FruitNotFoundException ex)
        {
            return BadRequest(new ApiResponse<string>(ex.Message));
        }
    }
}
