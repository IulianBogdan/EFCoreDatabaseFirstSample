using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreDatabaseFirstSample.Models.Repository
{
    public interface IDataRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        Task<string> Add(TEntity entity);
        Task<string> Update(TEntity entity);
        Task<string> Delete(int id);
        IEnumerable<TEntity> GetFictionBooks();
    }
}
