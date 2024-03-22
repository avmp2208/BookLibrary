using BookLibrary.Data;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Repository;

public interface IRepository
{
    Task<List<Book>> GetBookList(string column, string search);
}

public class Repository : IRepository
{
    private readonly BookLibraryContext _libraryContext;
    public Repository(BookLibraryContext libraryContext)
    {
        _libraryContext = libraryContext;
    }
    
    public async Task<List<Book>> GetBookList(string column, string search)
    {
        var predicate = PredicateBuilder.New<Book>();
        if (!string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(column))
        {
            switch (column.ToLower())
            {
                case "own/love/want":
                    predicate = predicate.And(b => b.Title.Contains(search));
                    break;
                case "author":
                    predicate = predicate.And(b => (b.FirstName + " " + b.LastName).Contains(search));
                    break;
                case "isbn":
                    predicate = predicate.And(b => b.Isbn.Contains(search));
                    break;
            }
            
            return await _libraryContext.Books.AsExpandable().Where(predicate).ToListAsync();
            
        }

        return await _libraryContext.Books.ToListAsync();
    }
}