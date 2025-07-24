using AutoMapper;
using RecipeShare.Application.DTOs;
using RecipeShare.Application.Interfaces;
using RecipeShare.Domain.Entities;
using RecipeShare.Infrastructure.Interfaces;
namespace RecipeShare.Application.Services;
public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IMapper _mapper;
    public RecipeService(IRecipeRepository recipeRepository, IMapper mapper)
    {
        _recipeRepository = recipeRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<RecipeDto>> GetAllAsync(string? tag)
    {
        var allRecipes = (await _recipeRepository.GetAllAsync()).ToList(); 
        if (!string.IsNullOrWhiteSpace(tag))
        {
            allRecipes = allRecipes
                .Where(r => !string.IsNullOrWhiteSpace(r.DietaryTags) &&
                            r.DietaryTags.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                         .Any(t => t.Trim().Equals(tag, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }
        return _mapper.Map<IEnumerable<RecipeDto>>(allRecipes);
    }
    public async Task<RecipeDto?> GetByIdAsync(Guid id)
    {
        var recipe = await _recipeRepository.GetByIdAsync(id);
        if (recipe == null)
            throw new KeyNotFoundException("Recipe not found.");
        return _mapper.Map<RecipeDto>(recipe);
    }
    public async Task<RecipeDto> CreateAsync(RecipeDto recipeDto)
    {
        if (recipeDto == null)
            throw new ArgumentNullException(nameof(recipeDto), "Recipe cannot be null.");
        var recipe = _mapper.Map<Recipe>(recipeDto);
        var createdRecipe = await _recipeRepository.CreateAsync(recipe);
        return _mapper.Map<RecipeDto>(createdRecipe);
    }
    public async Task<RecipeDto?> UpdateAsync(Guid id, RecipeUpdateDto recipeUpdateDto)
    {
        var existingRecipe = await _recipeRepository.GetByIdAsync(id);
        if (existingRecipe == null)
            throw new KeyNotFoundException("Recipe not found.");

        _mapper.Map(recipeUpdateDto, existingRecipe);
        var updated = await _recipeRepository.UpdateAsync(id, existingRecipe);
        return _mapper.Map<RecipeDto>(updated);
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await _recipeRepository.GetByIdAsync(id);
        if (existing == null)
            throw new KeyNotFoundException("Recipe not found.");
        return await _recipeRepository.DeleteAsync(id);
    }
}