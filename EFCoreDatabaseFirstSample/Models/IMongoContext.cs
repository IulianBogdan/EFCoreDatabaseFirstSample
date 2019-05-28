using MongoDB.Driver;

namespace EFCoreDatabaseFirstSample.Models
{
    public interface IMongoContext
    {
        IMongoCollection<Address> Address { get; set; }
        IMongoCollection<Author> Author { get; set; }
        IMongoCollection<AuthorContact> AuthorContact { get; set; }
        IMongoCollection<Book> Book { get; set; }
        IMongoCollection<BookAuthors> BookAuthors { get; set; }
        IMongoCollection<BookCategory> BookCategory { get; set; }
        IMongoCollection<Publisher> Publisher { get; set; }
    }
}