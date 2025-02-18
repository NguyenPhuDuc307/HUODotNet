using HUODotNet.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HUODotNet.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Product> Products { get; set; }
}