namespace ArtworkApp.Data;

using ArtworkApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

public class ArtworkAppDbContext : DbContext
{
    private readonly string _connectionString = @"Server=(localdb)\mssqllocaldb;Database=ArtworkAppDB";

    public DbSet<Painting> Paintings => Set<Painting>();

    public DbSet<Sculpture> Sculptures => Set<Sculpture>();
    public ArtworkAppDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(_connectionString);
        //optionsBuilder.UseInMemoryDatabase("StorageAppDb");
    }

}
