using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data;

public class BookLibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    
    
    public BookLibraryContext(DbContextOptions<BookLibraryContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookMap());
    }
}