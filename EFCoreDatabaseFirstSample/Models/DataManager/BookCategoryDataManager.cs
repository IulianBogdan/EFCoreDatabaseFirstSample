using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreDatabaseFirstSample.Models.Repository;

namespace EFCoreDatabaseFirstSample.Models.DataManager
{
    public class BookCategoryDataManager : IDataRepository<BookCategory>
    {
        readonly BookStoreContext _bookStoreContext;

        public BookCategoryDataManager(BookStoreContext storeContext)
        {
            _bookStoreContext = storeContext;
        }

        public IEnumerable<BookCategory> GetAll()
        {
            return _bookStoreContext.BookCategory.ToList();
        }

        public BookCategory Get(int id)
        {
            var bookCategory = _bookStoreContext.BookCategory
                .SingleOrDefault(b => b.Id == id);

            return bookCategory ?? null;
        }

        public async Task<string> Add(BookCategory entity)
        {
            _bookStoreContext.Add(entity);
            if (await _bookStoreContext.SaveChangesAsync() > 0)
            {
                return "Added";
            }

            return "There was a problem while adding the book category";
        }

        public async Task<string> Update(BookCategory entity)
        {
            var id = Get(entity.Id);
            if (id == null)
            {
                return "Book category not found";
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
                return "The book category does not exist in the database";
            }

            _bookStoreContext.BookCategory.Remove(exists);
            if (await _bookStoreContext.SaveChangesAsync() > 0)
            {
                return "Deleted";
            }

            return "There has been an error during deletion";
        }
    }
}
