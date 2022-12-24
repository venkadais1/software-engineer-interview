using Microsoft.AspNetCore.Mvc;
using System.Net;
using Zip.Installments.Infrastructure.Models;
using Zip.Installments.ViewModel.Orders;
using Zip.InstallmentsService.Helpers;
using Zip.InstallmentsService.Interface;

namespace Zip.Installments.API.Controllers.v1
{
    /// <summary>
    ///     The Definition of user orders controller
    /// </summary>
    [ApiController]
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
        [HttpGet("")]
        public async Task<IActionResult> GetOrders()
        {
            this.logger.LogInfo($"{nameof(OrdersController.GetOrders)} Started");

            try
            {
                var response = await this.orderService.GetOrders();
                return response == null ? this.NotFound() :
                    Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                this.logger.LogError($"Code:{HttpStatusCode.Unauthorized}:{ex}");
                return ObjectResponse.GetResults(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (AccessViolationException ex)
            {
                this.logger.LogError($"Code:{HttpStatusCode.Forbidden}:{ex}");
                return ObjectResponse.GetResults(HttpStatusCode.Forbidden, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                this.logger.LogError($"Code:{HttpStatusCode.BadRequest}:{ex}");
                return ObjectResponse.GetResults(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (InvalidDataException ex)
            {
                this.logger.LogError($"Code:{HttpStatusCode.Conflict}:{ex}");
                return ObjectResponse.GetResults(HttpStatusCode.Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Code:{HttpStatusCode.InternalServerError}:{ex}");
                return ObjectResponse.GetResults(HttpStatusCode.InternalServerError, ex.Message, true);
            }
            finally
            {
                this.logger.LogInfo($"{nameof(OrdersController.GetOrders)} END");
            }


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

            try
            {
                if (order == null)
                {
                    throw new ArgumentNullException("Invalid Order");
                }

                var response = await this.orderService.CreateOrder(order);

                return response == null ? this.NotFound() :
                    Ok(response);

            }
            catch (UnauthorizedAccessException ex)
            {
                this.logger.LogError($"Code:{HttpStatusCode.Unauthorized}:{ex}");
                return ObjectResponse.GetResults(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (AccessViolationException ex)
            {
                this.logger.LogError($"Code:{HttpStatusCode.Forbidden}:{ex}");
                return ObjectResponse.GetResults(HttpStatusCode.Forbidden, ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                this.logger.LogError($"Code:{HttpStatusCode.BadRequest}:{ex}");
                return ObjectResponse.GetResults(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                this.logger.LogError($"Code:{HttpStatusCode.BadRequest}:{ex}");
                return ObjectResponse.GetResults(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (InvalidDataException ex)
            {
                this.logger.LogError($"Code:{HttpStatusCode.Conflict}:{ex}");
                return ObjectResponse.GetResults(HttpStatusCode.Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Code:{HttpStatusCode.InternalServerError}:{ex}");
                //return ObjectResponse.GetResults(HttpStatusCode.Conflict, ex.Message, true);
                return ObjectResponse.GetResults(HttpStatusCode.InternalServerError, ex.ToString());
            }
            finally
            {
                this.logger.LogInfo($"{nameof(OrdersController.CreateOrders)} END");
            }
        }
    }
}
