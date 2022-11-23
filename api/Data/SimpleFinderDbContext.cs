using Microsoft.EntityFrameworkCore;
using SimpleFinder.Features.Files;

namespace SimpleFinder;

public class SimpleFinderDbContext : DbContext
{
    public DbSet<FileNode> FileNodes => Set<FileNode>();
    public DbSet<FileType> FileTypes => Set<FileType>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;");


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new FileNodeConfiguration());
        builder.ApplyConfiguration(new FileTypeConfiguration());
    }
}