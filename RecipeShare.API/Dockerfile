FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copy solution and all projects
COPY ["RecipeShare.API/RecipeShare.API.csproj", "RecipeShare.API/"]
COPY ["RecipeShare.Application/RecipeShare.Application.csproj", "RecipeShare.Application/"]
COPY ["RecipeShare.Domain/RecipeShare.Domain.csproj", "RecipeShare.Domain/"]
COPY ["RecipeShare.Infrastructure/RecipeShare.Infrastructure.csproj", "RecipeShare.Infrastructure/"]
# Restore NuGet packages
RUN dotnet restore "RecipeShare.API/RecipeShare.API.csproj"
# Copy the full source code
COPY . .
# Build the project
WORKDIR "/src/RecipeShare.API"
RUN dotnet publish -c Release -o /app/publish
# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
# Copy published app from build stage
COPY --from=build /app/publish .
# Set the entry point
ENTRYPOINT ["dotnet", "RecipeShare.API.dll"]