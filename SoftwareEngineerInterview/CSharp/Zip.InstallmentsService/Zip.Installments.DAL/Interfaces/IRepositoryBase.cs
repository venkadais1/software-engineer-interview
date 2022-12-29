using System.Linq.Expressions;

namespace Zip.Installments.DAL.Interfaces
{
    // <summary>
    ///     The repository interfaces
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBase<T>
    {
        /// <summary>
        ///     Find an instance by id
        /// </summary>
        /// <param name="id">The instance id</param>
        /// <returns>An instance</returns>
        Task<T> Find<TKey>(TKey id);

        /// <summary>
        ///     To Get all instances
        /// </summary>
        /// <param name="includes">Include the missing sub items in response</param>
        /// <returns>Returns list of instances</returns>
        Task<IList<T>> FindAll(params Expression<Func<T, object>>[] includes);

        /// <summary>
        ///     Find list of entities by condition and sort by order
        /// </summary>
        /// <param name="expression">The condition expression</param>
        /// <param name="orderPredicate">The order predicate</param>
        /// <param name="isAscendingOrder">Boolean to choose ascending or descending</param>
        /// <param name="includes">Fields to included in response if missing</param>
        /// <returns>Returns list of Instances</returns>
        Task<IList<T>> FindConditoin(Expression<Func<T, bool>> expression,
            Expression<Func<T, object>> orderPredicate,
            bool isAscendingOrder = false,
            params Expression<Func<T, object>>[] includes);

        /// <summary>
        ///     Create an entity
        /// </summary>
        /// <param name="entity">Instance of an entity</param>
        Task Create(T entity);

        /// <summary>
        ///     Update an entity
        /// </summary>
        /// <param name="entity">The new entity to update</param>
        /// <returns>Returns an updated entity</returns>
        Task<T> Update(T entity);

        /// <summary>
        ///     Delete an entity
        /// </summary>
        /// <param name="entity">An entity to delete</param>
        /// <returns>Returns the deleted count</returns>
        Task<int> Delete(T entity);

    }
}
