using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
namespace newWebAPI.Models;

public class AppDbContext : DbContext
{
    //private const string ConnectionString =@"Server=localhost;Database=BookDb;Trusted_Connection=True;";
    
    /*public AppDbContext(AppDbContextOptions<AppDbContext> options) : base(options)
    {

    }*/
    private const string ConnectionString = "Data Source=BookDb.db";
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite((ConnectionString));
        /*var currentDir = Directory.GetCurrentDirectory();
        var dbPath = dbPath.Combine(currentDir, 'BookDb.db');*/
    }

    public DbSet<Book> Books { get; set; }
}
