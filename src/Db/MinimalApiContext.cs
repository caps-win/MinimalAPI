using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Models;

namespace MinimalAPI.Db
{
  public class MinimalAPIContext : DbContext
  {
    public DbSet<Category> Categories { get; set; }
    public DbSet<Models.Task> Tasks { get; set; }
    public MinimalAPIContext(DbContextOptions<MinimalAPIContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      //Primary Keys
      builder.Entity<Category>()
      .HasKey(x => x.Id);

      builder.Entity<Models.Task>()
      .HasKey(x => x.Id);

      //Relationships
      builder.Entity<Models.Task>()
      .HasOne(x => x.Category)
      .WithMany(x => x.Tasks);

      // Constraints
      builder.Entity<Models.Task>()
      .Property(x => x.Title)
      .HasMaxLength(100);

      builder.Entity<Models.Category>()
      .Property(x => x.Name)
      .HasMaxLength(100);

      //seeding
      Guid CategoryId = Guid.NewGuid();
      builder.Entity<Models.Category>()
      .HasData(new Category() { Id = CategoryId, Name = "Work", Description = "Work time", IsEnabled = true });

      builder.Entity<Models.Task>()
      .HasData(new Models.Task() { Id = Guid.NewGuid(), Title = "Work", Description = "Work time", CreatedDate = DateTime.UtcNow.ToLocalTime(), CategoryId = CategoryId });
    }
  }
}