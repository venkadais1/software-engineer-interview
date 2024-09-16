using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Zip.Installments.Core.Interface;
using Zip.Installments.Core.Models;
using Zip.Installments.ViewModel.Orders;
using Zip.InstallmentsService.Interface;

namespace Zip.Installments.API.Controllers.v1
{
    /// <summary>
    ///     The Definition of user orders controller
    /// </summary>
    [ApiVersion("1")]
    public class OrdersController : ApiBaseController
    {
        private readonly IOrderService orderService;
        private readonly INLogger logger;

        /// <summary>
        ///     Initialize an instance Orders Controller
        /// </summary>
        /// <param name="orderService">An instance of Order Service</param>
        /// <param name="logger">An instance of Logger</param>
        public OrdersController(
            IOrderService orderService,
            INLogger logger)
        {
            this.orderService = orderService;
            this.logger = logger;
        }

        /// <summary>
        ///     GET: To get the list of user orders 
        /// </summary>
        /// <param name="order">An instance of <see cref="Order"/></param>
        /// <returns>Returns an instance of <see cref="OrderResponse"/></returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetOrders()
        {

            this.logger.LogInfo($"{nameof(OrdersController.GetOrders)} Started");

            var response = await this.orderService.GetOrders();

            this.logger.LogInfo($"{nameof(OrdersController.GetOrders)} END");
            return response == null ? this.NotFound() :
                Ok(response);
        }

        /// <summary>
        ///     GET: To get the list of user orders 
        /// </summary>
        /// <param name="order">An instance of order</param>
        /// <returns>Returns an instance of order response</returns>
        [HttpGet("filter")]
        public async Task<IActionResult> GetOrderByOrderByFilter([FromQuery]
            [Optional]string OrderId,
            [Optional] string Email,
            [Optional] string FirstName,
            [Optional] string LastName,
            [Optional] string OrderTitle)
        {
            this.logger.LogInfo($"{nameof(OrdersController.GetOrderByOrderByFilter)} Started");
            var response = await this.orderService.GetOrderByFilter(OrderId, Email, FirstName, LastName, OrderTitle);

            this.logger.LogInfo($"{nameof(OrdersController.GetOrderByOrderByFilter)} END");
            return response == null ? this.NotFound() :
                Ok(response);
        }

        /// <summary>
        ///     POST: Create user order 
        /// </summary>
        /// <param name="order">An instance of <see cref="Order"/></param>
        /// <returns>Returns an instance of <see cref="OrderResponse"/></returns>
        [HttpPost("")]
        public async Task<IActionResult> CreateOrders(
            [FromBody] OrdersViewModel order)
        {
            this.logger.LogInfo($"{nameof(OrdersController.GetOrders)} Started");

            var response = await this.orderService.CreateOrder(order);

            this.logger.LogInfo($"{nameof(OrdersController.GetOrders)} END");
            return response == null ? this.NotFound() :
                Ok(response);
        }
    }
}
