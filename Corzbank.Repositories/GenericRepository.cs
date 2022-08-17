using Corzbank.Data;
using Corzbank.Data.Models;
using Corzbank.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Corzbank.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly CorzbankDbContext _dbContext;

        public GenericRepository(CorzbankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbContext.Set<T>();
        }

        public async Task Insert(T entity, bool saveChanges = true)
        {
            _dbContext.Set<T>().Add(entity);
            if (saveChanges)
                await _dbContext.SaveChangesAsync();
        }

        public async Task InsertRange(IEnumerable<T> entities, bool saveChanges = true)
        {
            _dbContext.Set<T>().AddRange(entities);
            if (saveChanges)
                await _dbContext.SaveChangesAsync();
        }

        public async Task Remove(T entity, bool saveChanges = true)
        {
            _dbContext.Set<T>().Remove(entity);
            if (saveChanges)
                await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T entity, bool saveChanges = true)
        {
            _dbContext.Set<T>().Update(entity);
            if (saveChanges)
                await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRange(IEnumerable<T> entities, bool saveChanges = true)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            if (saveChanges)
                await _dbContext.SaveChangesAsync();
        }

        public void DetachLocal(Expression<Func<T, bool>> predicate)
        {
            var collection = _dbContext.Set<T>().Where(predicate).ToList();

            foreach (var item in collection)
            {
                if (item != null)
                    _dbContext.Entry(item).State = EntityState.Detached;
            }
        }
    }
}

