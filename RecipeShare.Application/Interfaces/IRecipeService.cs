using RecipeShare.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShare.Application.Interfaces
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeDto>> GetAllAsync(string? tag);
        Task<RecipeDto?> GetByIdAsync(Guid id);
        Task<RecipeDto> CreateAsync(RecipeDto recipeDto);
        Task<RecipeDto> UpdateAsync(Guid id, RecipeUpdateDto recipeUpdateDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
  