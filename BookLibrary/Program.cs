using BookLibrary.Data;
using BookLibrary.Helpers;
using BookLibrary.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookLibraryContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddSingleton<SearchLogger>();
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());


const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy  =>
        {
            policy.WithOrigins("*");
        });
});


var app = builder.Build();

RegisterEventSubscribers(app.Services);

app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BookLibraryContext>();
    dbContext.Database.Migrate();
}

app.Run();


void RegisterEventSubscribers(IServiceProvider serviceProvider)
{
    // Retrieve the instance of SearchLogger and subscribe to the BookSearched event
    var searchLogger = serviceProvider.GetService<SearchLogger>();
    BookLibrary.Controllers.BookLibrary.BookSearched += searchLogger!.HandleBookSearchedEvent;
}