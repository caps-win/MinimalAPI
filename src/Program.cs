using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Db;
using MinimalAPI.Models;
using MinimalAPI.Routing;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
string databaseCnx = configuration.GetValue<string>("ConnectionStrings:Source")!;

builder.Services.AddDbContext<MinimalAPIContext>(p => p.UseSqlServer(databaseCnx));
var app = builder.Build();

app.MapGroup("/api/v1/category").MapCategoriesApi();
app.MapGet("/", () => "Ok");

app.Run();