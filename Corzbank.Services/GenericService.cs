using Corzbank.Data;
using Corzbank.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Corzbank.Helpers.Constants;

namespace Corzbank.Services
{
    public class GenericService<T> where T : class
	{
		public readonly CorzbankDbContext _dbContext;

		public GenericService(CorzbankDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<int> SaveChanges()
		{
			return await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<T>> GetRange()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task<T> Get(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public async Task<T> GetByGuid(Guid id)
		{
			return await _dbContext.Set<T>().FindAsync(id.ToString());
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

		public Task<PagedList<TResult>> GetPagedListIncludeableOrderable<TResult>(Expression<Func<T, bool>> condition, Expression<Func<T, TResult>> selector,
															int? currentPage = Pagination.DefaultPage, int? itemsPerPage = Pagination.DefaultPageSize,
															Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
															Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)

		{
			var dataSet = _dbContext.Set<T>().Where(condition).AsNoTracking();

			if (include != null)
			{
				dataSet = include(dataSet);
			}
			if (orderBy != null)
			{
				dataSet = orderBy(dataSet).AsQueryable();
			}

			var dataList = dataSet
				.Skip((currentPage.Value - 1) * itemsPerPage.Value)
				.Take(itemsPerPage.Value)
				.AsNoTracking()
				.Select(selector);

			var pagedList = new PagedList<TResult>(currentPage.Value, itemsPerPage.Value, dataSet.Count(), dataList);
			return Task.FromResult(pagedList);
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

		public bool CheckByCondition(Expression<Func<T, bool>> expression)
        {
			var result = _dbContext.Set<T>().Any(expression);

			return result;
        }
	}
}
