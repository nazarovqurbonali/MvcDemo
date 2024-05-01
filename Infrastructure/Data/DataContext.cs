using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> options):base(options){}

    public DbSet<Market> Markets { get; set; } = null!;
    public DbSet<Product> Products { get; set; }= null!;
    public DbSet<Category> Categories { get; set; }= null!;

    protected override void OnModelCreating(ModelBuilder builder)
        => base.OnModelCreating(builder);
}