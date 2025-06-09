//using Bookish.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;


public class ZooDBContext : DbContext
{
    // Put all the tables you want in your database here
    public DbSet<Animal> Animals { get; set; }
    public DbSet<Zookeeper> Zookeepers { get; set; }
    public DbSet<Enclosure> Enclosures { get; set; }
    protected readonly IConfiguration _Configuration;
    
    /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            try
            {
                using var connection = new SqliteConnection(@"Server=localhost;Port=5432;Database=bookish;User Id=bookish;Password=bookish;");
                connection.Open();

                Console.WriteLine("Connected to the SQLite database!");
                connection.Close();

            }
            catch (SqliteException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        */


    public ZooDBContext(IConfiguration configuration)
    {
       _Configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(_Configuration.GetConnectionString("ZooDatabase"));
    
        }
   
}


