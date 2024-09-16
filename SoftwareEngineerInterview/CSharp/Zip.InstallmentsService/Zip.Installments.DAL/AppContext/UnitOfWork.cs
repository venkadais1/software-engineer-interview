using Microsoft.EntityFrameworkCore.Storage;
using Zip.Installments.DAL.Interfaces;

namespace Zip.Installments.DAL.AppContext
{
    /// <summary>
    ///     The repository wrapper for the facade class.
    /// </summary>
    public sealed class UnitOfWork : IUnityOfWork, IDisposable
    {
        private OrdersDbContext context;

        /// <summary>
        ///     Initialize an instance of <see cref="UnitOfWork"/>
        /// </summary>
        /// <param name="ordersDbContext">The db context</param>
        public UnitOfWork(OrdersDbContext ordersDbContext)
        {
            this.context = ordersDbContext;
            OrdersRepository = new OrdersRepository(context);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(
            CancellationToken cancellationToken = default)
            => await this.context.Database.BeginTransactionAsync(cancellationToken);

        public async Task CommitTransactionAsync(
            CancellationToken cancellationToken = default)
            => await this.context.Database.CommitTransactionAsync(cancellationToken);

        public async Task RollbackTransactionAsync(
            CancellationToken cancellationToken = default)
            => await this.context.Database.RollbackTransactionAsync(cancellationToken);

        /// <summary>
        ///     An instance of order repository
        /// </summary>
        public IOrdersRepository OrdersRepository { get; init; }

        /// <summary>
        ///     To save the db call
        /// </summary>
        /// <returns>Returns a task</returns>
        public async Task Save()
            => await this.context.SaveChangesAsync();


        public void Dispose()
        {
            if(this.context!=null)
            {
                this.context.Dispose();
                this.context = null;
            }
        }

    }
}
