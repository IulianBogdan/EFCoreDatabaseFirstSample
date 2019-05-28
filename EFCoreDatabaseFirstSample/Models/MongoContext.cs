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
        
        public IMongoCollection<Address> Address { get; set; }
        public IMongoCollection<Author> Author { get; set; }
        public IMongoCollection<AuthorContact> AuthorContact { get; set; }
        public IMongoCollection<Book> Book { get; set; }
        public IMongoCollection<BookAuthors> BookAuthors { get; set; }
        public IMongoCollection<BookCategory> BookCategory { get; set; }
        public IMongoCollection<Publisher> Publisher { get; set; }
    }
}