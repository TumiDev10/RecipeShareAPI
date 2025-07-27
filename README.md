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

## Unit Tests
Unit tests are written using `xUnit`.
### Run tests:
```bash
cd RecipeShare.Tests
dotnet test
---
### Developed by Tumi Mashigo
