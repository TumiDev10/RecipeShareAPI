using Microsoft.EntityFrameworkCore;
using RecipeShare.Domain.Entities;
using RecipeShare.Infrastructure.Data;
using RecipeShare.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShare.Infrastructure.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeDbContext _context;

        public RecipeRepository(RecipeDbContext context)
        {
            _context = context;
        }
        public async Task<List<Recipe>> GetAllAsync(string? tag = null) =>
            await _context.Recipes.ToListAsync();

        public async Task<Recipe?> GetByIdAsync(Guid id) =>
            await _context.Recipes.FindAsync(id);

        public async Task<Recipe> CreateAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe?> UpdateAsync(Guid id, Recipe recipe)
        {
            var existingRecipe = await _context.Recipes.FindAsync(id);

            if (existingRecipe == null) return null;

            _context.Entry(existingRecipe).CurrentValues.SetValues(recipe);
            await _context.SaveChangesAsync();
            return existingRecipe;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null) return false;

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
