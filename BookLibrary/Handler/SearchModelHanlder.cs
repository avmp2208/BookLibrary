using BookLibrary.Data;
using BookLibrary.Repository;
using MediatR;

namespace BookLibrary.Handler;

public class SearchModelHanlder : IRequestHandler<SearchModel, List<Book>>
{
    private readonly IRepository _repository;
    public SearchModelHanlder(IRepository dbContext)
    {
        _repository = dbContext;
    }
    
    public async Task<List<Book>> Handle(SearchModel request, CancellationToken cancellationToken)
    {
        return await _repository.GetBookList(request.SearchBy, request.Value);
    }
}