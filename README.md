# 🧑‍🍳 RecipeShare API (Backend)
## Overview
This is the backend API for RecipeShare, built with **ASP.NET Core 8**, **Entity Framework Core**, and **SQL Server**. It exposes RESTful endpoints for managing recipes and includes features such as validation, filtering, seeding, and unit testing.
---
## Features
- ✅ Full CRUD support for recipes
- 🔎 Filter by dietary tags
- 💾 Data persistence using EF Core
- 🧪 Validation (required fields, cooking time > 0)
- 🌱 Seeded database with 3 sample recipes
- 🧪 Unit tests using xUnit
- 🐞 Serilog-based logging
- 🚀 Benchmark performance test using `Stopwatch`
- 🧱 DDD-inspired folder structure
- 🐳 Docker-ready setup
---
## Getting Started
### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
### Running the Application
1. Clone the repository
2. Navigate to the backend folder:
   ```bash
   cd RecipeShare.API
   ```
3. Restore NuGet packages:
   ```bash
   dotnet restore
   ```
4. Apply database migrations:
   ```bash
   dotnet ef database update
   ```
5. Run the API:
   ```bash
   dotnet run
   ```
> 🧪 Visit the API Swagger UI at: `https://localhost:5000/swagger`
---
## Project Structure
RecipeShare.API
├── Controllers         # API endpoints
├── DTOs                # Request/response models
├── Services            # Business logic
├── Repositories        # Data access layer
├── Domain              # Core domain models
├── Infrastructure      # EF Core setup, DbContext, seed data
├── Program.cs          # App entry and middleware configRecipeShare.API
├── Controllers         # API endpoints
├── DTOs                # Request/response models
├── Services            # Business logic
├── Repositories        # Data access layer
├── Domain              # Core domain models
├── Infrastructure      # EF Core setup, DbContext, seed data
├── Program.cs          # App entry and middleware config

---
## API Endpoints
| Method | Route               | Description        |
|--------|---------------------|--------------------|
| GET    | /api/recipes        | Get all recipes    |
| GET    | /api/recipes/{id}   | Get a recipe by ID |
| POST   | /api/recipes        | Create a recipe    |
| PUT    | /api/recipes/{id}   | Update a recipe    |
| DELETE | /api/recipes/{id}   | Delete a recipe    |
---
## Validation Rules
- `Title`: Required
- `CookingTime`: Must be greater than 0
- `Ingredients` and `Steps`: Required
- `DietaryTags`: Optional string array
---
## Unit Tests
Unit tests are written using `xUnit`.
### Run tests:
```bash
cd RecipeShare.Tests
dotnet test
---
### Developed by Tumi Mashigo
