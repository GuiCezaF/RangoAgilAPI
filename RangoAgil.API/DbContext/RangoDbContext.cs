using Microsoft.EntityFrameworkCore;
using RangoAgil.API.Entities;

namespace RangoAgil.API.DbContext;

public class RangoDbContext(DbContextOptions<RangoDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<Rango> Rangos { get; set; } = null!;
    public DbSet<Ingrediente> Ingredientes { get; set; } = null!;

    protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}