using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boardgames.Repository
{
    public interface IRepository<TEntity, in TKey> where TEntity : class, new()
    {
        TEntity GetByKey(TKey key);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetByFilter(Func<TEntity, bool> filter);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
