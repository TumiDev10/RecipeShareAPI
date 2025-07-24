using RecipeShare.Domain.Entities;
using RecipeShare.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShare.Infrastructure.Persistence
{
    public class DbSeeder
    {
        public static void Seed(RecipeDbContext context)
        {
            if (!context.Recipes.Any())
            {
                context.Recipes.AddRange(
                    new Recipe
                    {
                        Title = "Southern Fried Chicken",
                        CookingTimeInMinutes = 45,
                        Ingredients = "Chicken pieces, buttermilk, hot sauce, flour, paprika, garlic powder, onion powder, salt, black pepper, cayenne, oil for frying",
                        Steps = "1. Marinate chicken in buttermilk and hot sauce. 2. Coat in seasoned flour. 3. Fry until golden brown and crispy.",
                        DietaryTags = "Comfort Food"
                    },
                    new Recipe
                    {
                        Title = "Pap and Wors",
                        CookingTimeInMinutes = 40,
                        Ingredients = "Maize meal, water, salt, boerewors sausage, onion, tomato, oil, garlic, curry powder, pepper",
                        Steps = "1. Bring water and salt to a boil. Slowly stir in maize meal to make pap. Simmer and stir until thick. 2. Braai or fry boerewors until cooked through. 3. For tomato relish, sauté onions, garlic, tomatoes, and spices in oil until thickened. Serve wors with pap and relish.",
                        DietaryTags = "Gluten-Free, Traditional"
                    },
                    new Recipe
                    {
                        Title = "Samp and Bean Stew (Umngqusho)",
                        CookingTimeInMinutes = 120,
                        Ingredients = "Samp, sugar beans, onion, garlic, beef stock, oil, carrots, potatoes, salt, black pepper, curry powder, bay leaves",
                        Steps = "1. Soak samp and beans overnight. Rinse and boil until soft. 2. In a separate pot, sauté onion and garlic, add carrots, potatoes, curry powder, and seasoning. 3. Add cooked samp and beans to the stew with stock. Simmer until thick and flavorful.",
                        DietaryTags = "Vegetarian-Friendly, Traditional"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
