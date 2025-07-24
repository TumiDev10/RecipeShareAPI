using RecipeShare.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShare.Infrastructure.Interfaces
{
    public interface IRecipeRepository
    {
        Task<List<Recipe>> GetAllAsync(string? tag = null);
        Task<Recipe?> GetByIdAsync(Guid id);
        Task<Recipe> CreateAsync(Recipe recipe);
        Task<Recipe?> UpdateAsync(Guid id, Recipe recipe);
        Task<bool> DeleteAsync(Guid id);
    }
}
