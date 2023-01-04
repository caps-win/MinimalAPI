using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Db;
using MinimalAPI.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
string dBName = configuration.GetValue<string>("DbName")!;

builder.Services.AddDbContext<MinimalAPIContext>(p => p.UseInMemoryDatabase(dBName));
var app = builder.Build();

app.MapGet("/", async ([FromServices] MinimalAPIContext ctx) =>
{
  await ctx.Database.EnsureCreatedAsync();
  var Categories = ctx.Categories.AsNoTracking().ToList();
  return Results.Ok(JsonConvert.SerializeObject(Categories));
});

object Ok(string v)
{
  throw new NotImplementedException();
}

app.Run();
