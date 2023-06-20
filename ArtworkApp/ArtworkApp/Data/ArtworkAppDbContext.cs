namespace ArtworkApp.Data;

using ArtworkApp.Entities;
using Microsoft.EntityFrameworkCore;

public class ArtworkAppDbContext : DbContext
{
    public DbSet<Painting> Paintings => Set<Painting>();

    public DbSet<Sculpture> Sculptures => Set<Sculpture>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase("StorageAppDb");
    }

}
