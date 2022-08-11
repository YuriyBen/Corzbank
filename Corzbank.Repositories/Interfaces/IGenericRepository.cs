using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<int> SaveChanges();

        IQueryable<T> GetQueryable();

        Task Insert(T entity, bool saveChanges = true);

        Task InsertRange(IEnumerable<T> entities, bool saveChanges = true);

        Task Remove(T entity, bool saveChanges = true);

        Task Update(T entity, bool saveChanges = true);

        Task UpdateRange(IEnumerable<T> entities, bool saveChanges = true);

        void DetachLocal(Expression<Func<T, bool>> predicate);
    }
}
