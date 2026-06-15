using AzureTest_benk.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Game> Game { get; set; }
}