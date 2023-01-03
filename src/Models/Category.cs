using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalAPI.Models
{
  public class Category
  {
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }

    public virtual ICollection<Task> Tasks { get; set; }
    public required bool IsEnabled { get; set; }

  }
}