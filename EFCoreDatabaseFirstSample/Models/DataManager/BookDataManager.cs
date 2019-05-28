using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreDatabaseFirstSample.Models.Repository;

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

        public async Task<string> Add(Book entity)
        {
            _bookStoreContext.Add(entity);
            if (await _bookStoreContext.SaveChangesAsync() > 0)
            {
                return "Added";
            }

            return "There was a problem while adding the book";
        }

        public async Task<string> Update(Book entity)
        {
            var id = Get(entity.Id);
            if (id == null)
            {
                return "Book not found";
            }

            _bookStoreContext.Update(entity);
            if (await _bookStoreContext.SaveChangesAsync() > 0)
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
            
            return "There has been an error during update";
        }
    }
}
