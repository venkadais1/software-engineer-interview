using Microsoft.EntityFrameworkCore.Storage;

namespace Zip.Installments.DAL.Interfaces
{
    /// <summary>
    ///     The repository wrapper for the facade interface.
    /// </summary>
    public interface IUnityOfWork
    {
        /// <summary>
        ///     An instance of order repository
        /// </summary>
        IOrdersRepository OrdersRepository { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IDbContextTransaction> BeginTransactionAsync(
            CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(
            CancellationToken cancellationToken = default); 
        Task RollbackTransactionAsync(
            CancellationToken cancellationToken = default);

        /// <summary>
        ///     To save the db call
        /// </summary>
        /// <returns>Returns a task</returns>
        Task Save();
    }
}
