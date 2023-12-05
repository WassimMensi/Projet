using Microsoft.EntityFrameworkCore;
namespace newWebAPI.Models;

public class AppContext : DbContext
{
    private const string ConnectionString =@"Server=localhost;Database=BookDb;Trusted_Connection=True;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
    }

    public DbSet<Book> Books { get; set }
}