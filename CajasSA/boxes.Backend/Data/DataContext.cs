using Microsoft.EntityFrameworkCore;
using Boxes.Shared.Entites;

namespace boxes.Backend.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Rol> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Rol>().HasIndex(c => c.Nombre).IsUnique();
    }
}