# For Event handlers.

We can add  for example an event handler to log the search 

    `public class SearchLogger
    {
    public SearchLogger()
    {
    BookLibrary.BookSearched += HandleBookSearchedEvent;
    }
    
        private void HandleBookSearchedEvent(object sender, SearchModel searchQuery)
        {
            // Log the search query
            Console.WriteLine($"Search query by '{searchQuery.SearchBy}' with value: '{searchQuery.Value}' logged at {DateTime.UtcNow}");
        }
    }`

We register it in the controller itself for example, creating a delegate and set ii up in the constructor, 
or just add it as a service from the builder at program start up

    `
        public delegate void BookSearchedEventHandler(object sender, SearchModel query);
    
        public static event BookSearchedEventHandler BookSearched;
    
        public BookLibrary(IRepository repository)
        {
            _repository = repository;
            RegisterEventSubscribers();
    
        }
    `

In this project I used only a simple demo of Repository pattern that 
consists in create an Interface that contains the services declared and a class with the db context to call the database,
But, we can use many pattern, I used to use for example Mediator because I like how it reduce the coupling between components, 
making the communication  better 

In this case for example we can create a Request class 

    `public class SearchModel : IRequest<List<Book>>
     {
         public string? SearchBy { get; set; }
         public string? Value { get; set; }
     }

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
    } `


And in the controller just inject the Mediator 

        `private readonly IMediator _mediator;
            
            //and for implementation
             
            var getBooks = await _mediator.Send(request);
        `

There are many approaches that we can use in this type of project but I did it as simple as possible thinking on a microservice that will be deployed to a k8s service 
