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

        /// <summary>
        ///     Create an entity
        /// </summary>
        /// <param name="entity">Instance of an entity</param>
        public async Task Create(T entity)
        {
            var id = await this.dbContext.Set<T>().AddAsync(entity);
        }

        /// <summary>
        ///     Find an instance by id
        /// </summary>
        /// <param name="id">The instance id</param>
        /// <returns>An instance</returns>
        public async Task<T> Find(Guid id)
        {
            return await this.dbContext.Set<T>().FindAsync(id);
        }

        /// <summary>
        ///     To Get all instances
        /// </summary>
        /// <param name="includes">Include the missing sub items in response</param>
        /// <returns>Returns list of instances</returns>
        public async Task<IList<T>> FindAll(params Expression<Func<T, object>>[] includes)
        {
            var query = this.dbContext.Set<T>()
                .IncludeMultiple(includes)
                .AsNoTracking()
                .AsQueryable();

            return await query.ToListAsync();
        }

        /// <summary>
        ///     Delete an entity
        /// </summary>
        /// <param name="entity">An entity to delete</param>
        /// <returns>Returns the deleted count</returns>
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

        /// <summary>
        ///     Update an entity
        /// </summary>
        /// <param name="entity">The new entity to update</param>
        /// <returns>Returns an updated entity</returns>
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

        /// <summary>
        ///     Find list of entities by condition and sort by order
        /// </summary>
        /// <param name="expression">The condition expression</param>
        /// <param name="orderPredicate">The order predicate</param>
        /// <param name="isAscendingOrder">Boolean to choose ascending or descending</param>
        /// <param name="includes">Fields to included in response if missing</param>
        /// <returns>Returns list of Instances</returns>
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
