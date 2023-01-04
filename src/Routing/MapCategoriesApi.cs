using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Db;
using MinimalAPI.Models;

namespace MinimalAPI.Routing
{
  public static class MapCrudCategoriesApi
  {
    public static IEndpointRouteBuilder MapCategoriesApi(this IEndpointRouteBuilder router)
    {
      router.MapGet("/", GetAllCategories);
      router.MapGet("/{id}", GetCategory);
      router.MapPost("/", CreateCategory);
      router.MapPut("/{id}", UpdateCategory);
      return router;
    }

    private static async Task<Category> UpdateCategory([FromServices] MinimalAPIContext ctx, HttpContext context, Guid id)
    {
      var request = await context.Request.ReadFromJsonAsync<Category>();
      var category = await ctx.Categories.FirstOrDefaultAsync(x => x.Id == id);

      category.Name = request.Name;
      category.Description = request.Description;
      category.IsEnabled = request.IsEnabled;
      await ctx.SaveChangesAsync();
      return category;
    }

    private static async Task<Category> CreateCategory([FromServices] MinimalAPIContext ctx, HttpContext context)
    {
      var request = await context.Request.ReadFromJsonAsync<Category>();
      Category category = new Category()
      {
        Id = Guid.NewGuid(),
        Name = request.Name,
        Description = request.Description,
        IsEnabled = true
      };

      await ctx.Categories.AddAsync(category);
      await ctx.SaveChangesAsync();
      return category;
    }

    private static async Task<Category> GetCategory([FromServices] MinimalAPIContext ctx, HttpContext context, Guid id)
    {
      return await ctx.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    private static async Task<IEnumerable<Category>> GetAllCategories([FromServices] MinimalAPIContext ctx, HttpContext context)
    {
      return await ctx.Categories.ToListAsync();
    }
  }
}