//using Bookish.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

public class ZooDBContext : DbContext
{
    // Put all the tables you want in your database here
    public DbSet<Animal> Animals { get; set; }
    public DbSet<Zookeeper> Zookeepers { get; set; }
    public DbSet<Enclosure> Enclosures { get; set; }
    public DbSet<Classification> Classifications { get; set; }
    public DbSet<Species> species { get; set; }
    protected readonly IConfiguration _Configuration;

    public ZooDBContext(IConfiguration configuration)
    {
        _Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(_Configuration.GetConnectionString("ZooDatabase"));
    }
}
