using BookLibrary.Data;

namespace BookLibrary.Repository;

public interface IRepository
{
    Task<List<Book>> GetBookList(string column, string search);
}