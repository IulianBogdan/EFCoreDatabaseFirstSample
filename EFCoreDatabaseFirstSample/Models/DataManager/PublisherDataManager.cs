using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreDatabaseFirstSample.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDatabaseFirstSample.Models.DataManager
{
    public class PublisherDataManager : IDataRepository<Publisher>
    {
        readonly BookStoreContext _bookStoreContext;

        public PublisherDataManager(BookStoreContext storeContext)
        {
            _bookStoreContext = storeContext;
        }

        // stored procedure SelectAllPublishers
        public IEnumerable<Publisher> GetAll()
        {
            return _bookStoreContext.Publisher
                .FromSql("EXECUTE dbo.SelectAllPublishers")
                .ToList();
        }

        public Publisher Get(int id)
        {
            var publisher = _bookStoreContext.Publisher
                .SingleOrDefault(a => a.Id == id);

            return publisher ?? null;
        }

        public async Task<string> Add(Publisher entity)
        {
            _bookStoreContext.Add(entity);
            if (await _bookStoreContext.SaveChangesAsync() > 0)
            {
                return "Added";
            }

            return "There was a problem while adding the Publisher";
        }

        public async Task<string> Update(Publisher entity)
        {
            var id = Get(entity.Id);
            if (id == null)
            {
                return "Publisher not found";
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
                return "The Publisher does not exist in the database";
            }

            _bookStoreContext.Publisher.Remove(exists);
            if (await _bookStoreContext.SaveChangesAsync() > 0)
            {
                return "Deleted";
            }
            
            return "There has been an error during update";
        }
    }
}
