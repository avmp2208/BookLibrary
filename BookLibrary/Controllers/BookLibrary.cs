using BookLibrary.Data;
using BookLibrary.Handler;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers;

[ApiController]
[Route("[controller]")]
public class BookLibrary : ControllerBase
{
    public delegate void BookSearchedEventHandler(object sender, SearchModel query);
    public static event BookSearchedEventHandler? BookSearched;
    private readonly IMediator _mediator;

    public BookLibrary(IMediator mediator)
    {
        _mediator = mediator;
    }


    //Get endpoint to get the book based on a search Author, ISBN, or Book title, receives two parameters Search and Value
    [HttpGet]
    [Route("GetBookList")]
    public async Task<ActionResult<List<Book>?>> GetBookList([FromQuery] SearchModel query)
    {
        OnBookSearched(query);


        var bookList = await _mediator.Send(query);
        if (!bookList.Any())
        {
            return NotFound();
        }

        return bookList;
    }

    protected virtual void OnBookSearched(SearchModel searchQuery)
    {
        BookSearched?.Invoke(this, searchQuery);
    }
}