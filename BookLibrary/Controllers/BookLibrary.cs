using BookLibrary.Data;
using BookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers;

[ApiController]
[Route("[controller]")]
public class BookLibrary : ControllerBase
{
    private readonly IRepository _repository;

    public BookLibrary(IRepository repository)
    {
        _repository = repository;
    }


    //Get endpoint to get the book based on a search Author, ISBN, or Book title, receives two parameters Search and Value
    [HttpGet]
    [Route("GetBookList")]
    public async Task<ActionResult<List<Book>?>> GetBookList([FromQuery] SearchModel query)
    {
        var bookList = await _repository.GetBookList(query.SearchBy, query.Value);
        if (!bookList.Any())
        {
            return NotFound();
        }
        return bookList;
    }
   
}