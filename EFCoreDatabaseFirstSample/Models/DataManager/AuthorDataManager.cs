using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreDatabaseFirstSample.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDatabaseFirstSample.Models.DataManager
{
    public class AuthorDataManager : IDataRepository<Author>
    {
        private readonly BookStoreContext _bookStoreContext;

        public AuthorDataManager(BookStoreContext storeContext)
        {
            _bookStoreContext = storeContext;
        }

        public IEnumerable<Author> GetAll()
        {
            return _bookStoreContext.Author.ToList();
        }

        public Author Get(int id)
        {
            var author = _bookStoreContext.Author
                .SingleOrDefault(a => a.Id == id);

            return author ?? null;
        }

        public async Task<string> Add(Author entity)
        {
            _bookStoreContext.Add(entity);
            if (await _bookStoreContext.SaveChangesAsync() > 0)
            {
                return "Added";
            }

            return "There was a problem while adding the author";
        }

        public async Task<string> Update(Author entity)
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
                return "The author does not exist in the database";
            }

            _bookStoreContext.Author.Remove(exists);
            if (await _bookStoreContext.SaveChangesAsync() > 0)
            {
                return "Deleted";
            }
            
            return "There has been an error during update";
        }

        public IEnumerable<Author> GetFictionBooks()
        {
            return null;
        }
    }
}
