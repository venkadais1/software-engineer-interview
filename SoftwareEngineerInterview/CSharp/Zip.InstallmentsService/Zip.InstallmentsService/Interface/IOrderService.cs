using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Installments.Core.Models;
using Zip.Installments.ViewModel.Orders;

namespace Zip.InstallmentsService.Interface
{
    /// <summary>
    ///     The Definition of Order Services
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        ///     Create the order of payment with installments
        /// </summary>
        /// <param name="order">An view model of order</param>
        /// <returns>Return created order</returns>
        Task<OrderResponse> CreateOrder(OrdersViewModel order);

        /// <summary>
        ///     Get the orders by condition
        /// </summary>
        /// <param name="filter">Search condition</param>
        /// <returns>Returns list of orders</returns>
        Task<IList<Order>> GetOrderByFilter(
            string OrderId,
            string Email,
            string FirstName,
            string LastName,
            string OrderTitle);

        /// <summary>
        ///     Get the list of orders
        /// </summary>
        /// <returns>Returns list of orders</returns>
        Task<IList<Order>> GetOrders();
    }
}
