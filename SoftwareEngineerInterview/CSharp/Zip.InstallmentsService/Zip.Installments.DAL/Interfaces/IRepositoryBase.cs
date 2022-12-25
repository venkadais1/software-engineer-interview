using System.Linq.Expressions;

namespace Zip.Installments.DAL.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<T> Find(Guid id);

        Task<IList<T>> FindAll(params Expression<Func<T, object>>[] includes);

        Task<IList<T>> FindConditoin(Expression<Func<T, bool>> expression,
            Expression<Func<T, object>> orderPredicate,
            bool isAscendingOrder = false,
            params Expression<Func<T, object>>[] includes);

        Task Create(T entity);

        Task<T> Update(T entity);

        Task<int> Delete(T entity);

    }
}
