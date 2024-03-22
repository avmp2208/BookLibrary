using BookLibrary.Handler;

namespace BookLibrary.Helpers;

public class SearchLogger
{
   
    public void HandleBookSearchedEvent(object sender, SearchModel searchQuery)
    {
        // Log the search query
        Console.WriteLine($"Search query by '{searchQuery.SearchBy}' with value: '{searchQuery.Value}' logged at {DateTime.UtcNow}");
    }
}