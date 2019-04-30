using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EFCoreDatabaseFirstSample.Models
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _db;

        public MongoContext(IOptions<MongoSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }
        
        public IMongoCollection<Author> Authors { get; }
        public IMongoCollection<AuthorContact> AuthorsContact { get; }
        public IMongoCollection<Book> Books { get; }
        public IMongoCollection<BookAuthors> BookAuthors { get; }
        public IMongoCollection<Publisher> Publishers { get; }
        public IMongoCollection<BookCategory> BookCategories { get; }
    }
}