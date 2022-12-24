﻿using Microsoft.AspNetCore.Mvc;
using System.Net;
using Zip.Installments.Infrastructure.Models;
using Zip.Installments.ViewModel.Orders;
using Zip.InstallmentsService.Helpers;
using Zip.InstallmentsService.Interface;

namespace Zip.Installments.API.Controllers.v2
{
    /// <summary>
    ///     The Definition of user orders controller
    /// </summary>
    [ApiController]
    [ApiVersion("2")]
    public class OrdersController : ApiBaseController
    {
        private readonly IOrderService orderService;
        private readonly ILogger<OrdersController> logger;

        public OrdersController(
            IOrderService orderService,
            ILogger<OrdersController> logger)
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

            try
            {
                var response = await this.orderService.GetOrders();
                return response == null ? this.NotFound() :
                    Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return ObjectResponse.GetResults(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (AccessViolationException ex)
            {
                return ObjectResponse.GetResults(HttpStatusCode.Forbidden, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return ObjectResponse.GetResults(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (InvalidDataException ex)
            {
                return ObjectResponse.GetResults(HttpStatusCode.Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message, ex);
                return ObjectResponse.GetResults(HttpStatusCode.Conflict, ex.Message, true);
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
                var response = await this.orderService.CreateOrder(order);
                return response == null ? this.NotFound() :
                    Ok(response);

            }
            catch (UnauthorizedAccessException ex)
            {
                return ObjectResponse.GetResults(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (AccessViolationException ex)
            {
                return ObjectResponse.GetResults(HttpStatusCode.Forbidden, ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return ObjectResponse.GetResults(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return ObjectResponse.GetResults(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (InvalidDataException ex)
            {
                return ObjectResponse.GetResults(HttpStatusCode.Conflict, ex.Message);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message, ex);
                //return ObjectResponse.GetResults(HttpStatusCode.Conflict, ex.Message, true);
                return ObjectResponse.GetResults(HttpStatusCode.InternalServerError, ex.ToString());
            }

        }
    }
}