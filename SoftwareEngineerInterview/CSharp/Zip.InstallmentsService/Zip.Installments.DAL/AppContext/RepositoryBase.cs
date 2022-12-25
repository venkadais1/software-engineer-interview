using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Zip.Installments.DAL.Extensions;
using Zip.Installments.DAL.Interfaces;

namespace Zip.Installments.DAL.AppContext
{
    /// <summary>
    ///     The repository base class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        private readonly OrdersDbContext dbContext;

        /// <summary>     
        ///     Initialize an <see cref="RepositoryBase"/>
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositoryBase(OrdersDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Create(T entity)
        {
            var id = await this.dbContext.Set<T>().AddAsync(entity);
        }

        public async Task<T> Find(Guid id)
        {
            var query = await this.dbContext.Set<T>().FindAsync(id);
            return query;
        }

        public async Task<IList<T>> FindAll(params Expression<Func<T, object>>[] includes)
        {
            var query = this.dbContext.Set<T>()
                .IncludeMultiple(includes)
                .AsNoTracking()
                .AsQueryable();

            return await query.ToListAsync();
        }

        public async Task<int> Delete(T entity)
        {
            int ret = 0;
            var record = await this.dbContext.Set<T>().FindAsync(entity);
            if (record != null)
            {
                this.dbContext.Set<T>().Remove(entity);
            }

            return ret;
        }

        public async Task<T> Update(T entity)
        {
            T ret = null;
            var record = await this.dbContext.Set<T>().FindAsync(entity);
            if (record != null)
            {
                this.dbContext.Set<T>().Update(entity);
                ret = record;
            }

            return ret;
        }

        public async Task<IList<T>> FindConditoin(
            Expression<Func<T, bool>> expression,
            Expression<Func<T, object>> orderPredicate,
            bool isAscendingOrder = false,
            params Expression<Func<T, object>>[] includes)
        {
            var query = this.dbContext.Set<T>()
                            .IncludeMultiple(includes)
                            .Where(expression)
                            .AsNoTracking()
                            .AsQueryable();

            query = isAscendingOrder ?
                    query.OrderBy(orderPredicate) :
                    query.OrderByDescending(orderPredicate);

            return await query.ToListAsync();
        }
    }
}
