using AutoMapper;
using Moq;
using RecipeShare.Application.DTOs;
using RecipeShare.Application.Services;
using RecipeShare.Domain.Entities;
using RecipeShare.Infrastructure.Interfaces;
using Xunit;
namespace RecipeShare.Application.Tests
{
    public class RecipeServiceTests
    {
        private readonly Mock<IRecipeRepository> _recipeRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly RecipeService _service;
        public RecipeServiceTests()
        {
            _recipeRepoMock = new Mock<IRecipeRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new RecipeService(_recipeRepoMock.Object, _mapperMock.Object);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnRecipes_WhenTagMatches()
        {
            // Arrange
            var tag = "vegan";
            var recipes = new List<Recipe>
           {
               new Recipe
               {
                   Id = Guid.NewGuid(),
                   Title = "Pap and Chakalaka",
                   Ingredients = "Pap, Chakalaka",
                   DietaryTags = "vegan, spicy",
                   Steps = "Cook pap, add chakalaka",
                   CookingTimeInMinutes = 20
               }
           };
            _recipeRepoMock.Setup(r => r.GetAllAsync(null)).ReturnsAsync(recipes);
            _mapperMock.Setup(m => m.Map<IEnumerable<RecipeDto>>(It.IsAny<IEnumerable<Recipe>>()))
                       .Returns(recipes.Select(r => new RecipeDto { Title = r.Title }));
            // Act
            var result = await _service.GetAllAsync(tag);
            // Assert
            Assert.Single(result);
            Assert.Equal("Pap and Chakalaka", result.First().Title);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnRecipe_WhenExists()
        {
            var id = Guid.NewGuid();
            var recipe = new Recipe
            {
                Id = id,
                Title = "Boerewors Roll",
                Ingredients = "Boerewors, Bread Roll",
                Steps = "Braai, assemble",
                CookingTimeInMinutes = 15
            };
            _recipeRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(recipe);
            _mapperMock.Setup(m => m.Map<RecipeDto>(recipe)).Returns(new RecipeDto { Title = recipe.Title });
            var result = await _service.GetByIdAsync(id);
            Assert.NotNull(result);
            Assert.Equal("Boerewors Roll", result.Title);
        }
        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedRecipe()
        {
            var dto = new RecipeDto
            {
                Title = "Chicken Feet Stew",
                Ingredients = "Chicken feet, spices",
                Steps = "Boil, season, simmer"
            };
            var recipe = new Recipe { Title = dto.Title };
            var created = new Recipe { Title = dto.Title };
            _mapperMock.Setup(m => m.Map<Recipe>(dto)).Returns(recipe);
            _recipeRepoMock.Setup(r => r.CreateAsync(recipe)).ReturnsAsync(created);
            _mapperMock.Setup(m => m.Map<RecipeDto>(created)).Returns(new RecipeDto { Title = dto.Title });
            var result = await _service.CreateAsync(dto);
            Assert.NotNull(result);
            Assert.Equal("Chicken Feet Stew", result.Title);
        }
        [Fact]
        public async Task UpdateAsync_ShouldReturnUpdatedRecipe_WhenExists()
        {
            var id = Guid.NewGuid();
            var updateDto = new RecipeUpdateDto { Title = "Updated Samp & Beans" };
            var existing = new Recipe { Id = id, Title = "Samp & Beans" };
            var updated = new Recipe { Id = id, Title = updateDto.Title };
            _recipeRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(existing);
            _mapperMock.Setup(m => m.Map(updateDto, existing));
            _recipeRepoMock.Setup(r => r.UpdateAsync(id, existing)).ReturnsAsync(updated);
            _mapperMock.Setup(m => m.Map<RecipeDto>(updated)).Returns(new RecipeDto { Title = updated.Title });
            var result = await _service.UpdateAsync(id, updateDto);
            Assert.NotNull(result);
            Assert.Equal("Updated Samp & Beans", result.Title);
        }
        [Fact]
        public async Task DeleteAsync_ShouldDelete_WhenExists()
        {
            var id = Guid.NewGuid();
            var existing = new Recipe { Id = id, Title = "Maotwana" };
            _recipeRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(existing);
            _recipeRepoMock.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);
            var result = await _service.DeleteAsync(id);
            Assert.True(result);
            _recipeRepoMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }
        [Fact]
        public async Task GetAllAsync_ShouldThrow_WhenNoResults()
        {
            // Arrange
            _recipeRepoMock.Setup(r => r.GetAllAsync(null)).ReturnsAsync(new List<Recipe>());
            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetAllAsync("vegan"));
        }
        [Fact]
        public async Task GetByIdAsync_ShouldThrow_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _recipeRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Recipe?)null);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(id));
        }
        [Fact]
        public async Task CreateAsync_ShouldThrow_WhenDtoIsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreateAsync(null));
        }
        [Fact]
        public async Task UpdateAsync_ShouldThrow_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _recipeRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Recipe?)null);
            var dto = new RecipeUpdateDto { Title = "Nonexistent" };
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateAsync(id, dto));
        }
        [Fact]
        public async Task DeleteAsync_ShouldThrow_WhenNotFound()
        {
            var id = Guid.NewGuid();
            _recipeRepoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Recipe?)null);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteAsync(id));
        }
    }
}