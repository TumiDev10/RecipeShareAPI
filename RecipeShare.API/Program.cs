using Microsoft.Extensions.DependencyInjection;
using RecipeShare.Application.Interfaces;
using RecipeShare.Application.Services;
using RecipeShare.Infrastructure.Data;
using RecipeShare.Infrastructure.Interfaces;
using RecipeShare.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using RecipeShare.API.Mapping;
using RecipeShare.Infrastructure.Persistence;
using Serilog;
using RecipeShare.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .WriteTo.Console()
        .ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.
// EF + DB Context
builder.Services.AddDbContext<RecipeDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// AutoMapper
builder.Services.AddAutoMapper(typeof(RecipeProfile));
// DI Bindings
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
// API Setup
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var app = builder.Build();
app.UseCors("AllowFrontend");
// Middleware registration
app.UseMiddleware<GlobalExceptionMiddleware>(); 

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RecipeDbContext>();
    dbContext.Database.Migrate();
    DbSeeder.Seed(dbContext);
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();

