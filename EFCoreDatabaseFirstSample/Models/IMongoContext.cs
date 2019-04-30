using MongoDB.Driver;

namespace EFCoreDatabaseFirstSample.Models
{
    public interface IMongoContext
    {
        IMongoCollection<Author> Authors { get; }
        IMongoCollection<AuthorContact> AuthorsContact { get; }
        IMongoCollection<Book> Books { get; }
        IMongoCollection<BookAuthors> BookAuthors { get; }
        IMongoCollection<Publisher> Publishers { get; }
        IMongoCollection<BookCategory> BookCategories { get; }
    }
}