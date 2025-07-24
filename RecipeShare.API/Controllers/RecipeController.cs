using Microsoft.AspNetCore.Mvc;
using RecipeShare.Application.DTOs;
using RecipeShare.Application.Interfaces;

namespace RecipeShare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly IRecipeService _recipeService;
    public RecipesController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? tag)
    {
        var recipes = await _recipeService.GetAllAsync(tag);
        return Ok(recipes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var recipe = await _recipeService.GetByIdAsync(id);
        if (recipe == null)
            return NotFound();
        return Ok(recipe);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RecipeDto recipeDto)
    {
        var created = await _recipeService.CreateAsync(recipeDto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] RecipeUpdateDto recipeUpdateDto)
    {
        await _recipeService.UpdateAsync(id, recipeUpdateDto);
        return Ok(recipeUpdateDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _recipeService.DeleteAsync(id);
        return NoContent();
    }
}