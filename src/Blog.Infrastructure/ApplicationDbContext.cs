using Blog.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure;

internal sealed class ApplicationDbContext : DbContext
{
  public DbSet<BlogPost> Posts { get; set; } = default!;
  public DbSet<PostSchedule> Schedules { get; set; } = default!;

  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  { }
}