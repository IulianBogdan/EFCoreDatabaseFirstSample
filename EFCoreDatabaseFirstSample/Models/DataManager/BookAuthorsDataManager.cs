using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreDatabaseFirstSample.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDatabaseFirstSample.Models.DataManager
{
    public class BookAuthorDataManager : IDataRepository<BookAuthors>
    {
        private readonly BookStoreContext _bookStoreContext;

        public BookAuthorDataManager(BookStoreContext storeContext)
        {
            _bookStoreContext = storeContext;
        }

        public IEnumerable<BookAuthors> GetAll()
        {
            return _bookStoreContext.BookAuthors.ToList();
        }

        public BookAuthors Get(int id)
        {
            var authors = _bookStoreContext.BookAuthors
                .SingleOrDefault(a => a.AuthorId == id);

            return authors ?? null;
        }

        public async Task<string> Add(BookAuthors entity)
        {
            var result = await _bookStoreContext.Database.ExecuteSqlCommandAsync("EXECUTE dbo.add_bookAuthors_deadlock @p0, @p1", entity.BookId, entity.AuthorId);

            if (result > 0)
            {
                return "Updated";
            }

            return "There was a problem while adding the author";
        }

        public async Task<string> Update(BookAuthors entity)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookAuthors> GetFictionBooks()
        {
            throw new NotImplementedException();
        }
    }
}