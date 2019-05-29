using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreDatabaseFirstSample.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDatabaseFirstSample.Models.DataManager
{
    public class BookDataManager : IDataRepository<Book>
    {
        readonly BookStoreContext _bookStoreContext;

        public BookDataManager(BookStoreContext storeContext)
        {
            _bookStoreContext = storeContext;
        }

        public IEnumerable<Book> GetAll()
        {
            return _bookStoreContext.Book.ToList();
        }

        public Book Get(int id)
        {
            var book = _bookStoreContext.Book
                .SingleOrDefault(b => b.Id == id);

            return book ?? null;
        }

        // stored procedure SelectBooksFromFictionCategory
        public List<Book> GetFictionBooks()
        {
            var fictionBooks = _bookStoreContext.Book
                .FromSql("EXECUTE SelectBooksFromFictionaryCategory")
                .ToList();

            return fictionBooks;
        }

        public async Task<string> Add(Book entity)
        {
            _bookStoreContext.Add(entity);
            if (await _bookStoreContext.SaveChangesAsync() > 0)
            {
                return "Added";
            }

            return "There was a problem while adding the book";
        }

        // stored procedure update_with_lock
        public async Task<string> Update(Book entity)
        {
            var id = Get(entity.Id);
            if (id == null)
            {
                return "Book not found";
            }

            var result = await _bookStoreContext.Database.ExecuteSqlCommandAsync("EXECUTE dbo.update_with_lock {0} {1} {2} {3} {4} {5} {6}",
                entity.Id, entity.Title, entity.CategoryId, entity.PublisherId, entity.Isbn, entity.PublicationYear, entity.Summary);

            if (result > 0)
            {
                return "Updated";
            }

            return "There has been an error during update";
        }

        public async Task<string> Delete(int id)
        {
            var exists = Get(id);
            if (exists == null)
            {
                return "The book does not exist in the database";
            }

            _bookStoreContext.Book.Remove(exists);
            if (await _bookStoreContext.SaveChangesAsync() > 0)
            {
                return "Deleted";
            }
            
            return "There has been an error during deletion";
        }
    }
}
