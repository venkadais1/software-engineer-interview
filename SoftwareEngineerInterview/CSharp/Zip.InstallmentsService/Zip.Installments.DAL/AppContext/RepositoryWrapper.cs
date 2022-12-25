using Zip.Installments.DAL.Interfaces;

namespace Zip.Installments.DAL.AppContext
{
    /// <summary>
    ///     The repository wrapper for the facade class.
    /// </summary>
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private OrdersDbContext _context;
        private IOrdersRepository _ordersRepository;

        /// <summary>
        ///     Initialize an instance of <see cref="RepositoryWrapper"/>
        /// </summary>
        /// <param name="ordersDbContext">The db context</param>
        public RepositoryWrapper(OrdersDbContext ordersDbContext)
        {
            _context = ordersDbContext;
        }

        /// <summary>
        ///     An instance of order repository
        /// </summary>
        public IOrdersRepository OrdersRepository
        {
            get
            {
                if (_ordersRepository == null)
                {
                    _ordersRepository = new OrdersRepository(_context);
                }
                return _ordersRepository;
            }
        }

        /// <summary>
        ///     To save the db call
        /// </summary>
        /// <returns>Returns a task</returns>
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
