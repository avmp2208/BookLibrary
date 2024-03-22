using BookLibrary.Data;
using MediatR;

namespace BookLibrary.Handler;

public class SearchModel : IRequest<List<Book>>
{
    public string? SearchBy { get; set; }
    public string? Value { get; set; }
}