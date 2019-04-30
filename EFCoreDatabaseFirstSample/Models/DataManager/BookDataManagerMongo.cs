using System.Collections.Generic;
using System.Linq;
using EFCoreDatabaseFirstSample.Models.DTO;
using EFCoreDatabaseFirstSample.Models.Repository;
using MongoDB.Driver;

namespace EFCoreDatabaseFirstSample.Models.DataManager
{
    public class BookDataManagerMongo : IDataRepository<Book, BookDto>
    {
        readonly MongoContext _mongoContext;

        public BookDataManagerMongo(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public IEnumerable<Book> GetAll()
        {
            return _mongoContext
                .Books
                .Find(_ => true)
                .ToList();
        }

        public Book Get(long id)
        {
            var filter = Builders<Book>.Filter.Eq(m => m.Id, id);

            return _mongoContext
                .Books
                .Find(filter)
                .FirstOrDefault();
        }

        public BookDto GetDto(long id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Book entity)
        {
            _mongoContext.Books.InsertOneAsync(entity);
        }

        public void Update(Book entityToUpdate, Book entity)
        {
            var updateResult =
                _mongoContext
                    .Books
                    .ReplaceOneAsync(
                        filter: b => b.Id == entity.Id,
                        replacement: entity);
        }

        public void Delete(Book entity)
        {
            var filter = Builders<Book>.Filter.Eq(b => b.Id, entity.Id);
            var deleteResult = _mongoContext
                .Books
                .DeleteOne(filter);
        }
    }
}