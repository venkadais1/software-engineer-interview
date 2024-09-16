using Zip.Installments.Core.Models;

namespace Zip.Installments.DAL.Interfaces
{
    /// <summary>
    ///     Facade interface to connect with Base repository by <see cref="T"/>
    /// </summary>
    public interface IOrdersRepository : IRepositoryBase<Order>
    {
    }
}
