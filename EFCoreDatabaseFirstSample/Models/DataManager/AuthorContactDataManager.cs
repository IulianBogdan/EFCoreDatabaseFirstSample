using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreDatabaseFirstSample.Models.Repository;

namespace EFCoreDatabaseFirstSample.Models.DataManager
{
    public class AuthorContactDataManager : IDataRepository<AuthorContact>
    {
        private readonly BookStoreContext _bookStoreContext;

        public AuthorContactDataManager(BookStoreContext storeContext)
        {
            _bookStoreContext = storeContext;
        }

        public IEnumerable<AuthorContact> GetAll()
        {
            var authorContacts = _bookStoreContext.AuthorContact.ToList();

            return authorContacts;
        }

        public AuthorContact Get(int id)
        {
            var authorContact = _bookStoreContext.AuthorContact
                .SingleOrDefault(a => a.AuthorContactId == id);

            return authorContact ?? null;
        }

        public async Task<string> Add(AuthorContact entity)
        {
            _bookStoreContext.Add(entity);
            if (await _bookStoreContext.SaveChangesAsync() > 0)
            {
                return "Added";
            }

            return "There was a problem while adding the author contact";
        }

        public async Task<string> Update(AuthorContact entity)
        {
            var id = Get(entity.AuthorContactId);
            if (id == null)
            {
                return "Author contact not found";
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
                return "The author contact does not exist in the database";
            }

            _bookStoreContext.AuthorContact.Remove(exists);
            if (await _bookStoreContext.SaveChangesAsync() > 0)
            {
                return "Deleted";
            }

            return "There has been an error during deletion";
        }

        public IEnumerable<AuthorContact> GetFictionBooks()
        {
            return null;
        }
    }
}
