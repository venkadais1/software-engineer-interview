using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Zip.Installments.API.Controllers.v1;
using Zip.Installments.Core.Interface;
using Zip.Installments.Core.Models;
using Zip.Installments.ViewModel.Orders;
using Zip.InstallmentsService.Interface;

namespace Zip.Installments.API.Tests.Controllers
{
    public class OrdersControllerTest
    {
        private readonly Mock<IOrderService> orderService;
        private readonly Mock<INLogger> logger;
        private readonly OrdersController ordersController;
        public OrdersControllerTest()
        {
            this.orderService = new Mock<IOrderService>();
            this.logger = new Mock<INLogger>();
            this.ordersController = new OrdersController(
                this.orderService.Object,
                this.logger.Object);
        }

        /// <summary>
        ///     Test Return status code 404 if request not found
        /// </summary>
        /// <returns>Returns status 404</returns>
        [Fact]
        public async Task CreateOrders_Returns_Status404()
        {
            // Arrange
            OrdersViewModel order = null;
            // Act
            var response = await this.ordersController.CreateOrders(order);

            //Assert

            Assert.NotNull(response);
            Assert.Equal((int)HttpStatusCode.NotFound, ((NotFoundResult)response).StatusCode);
        }

        /// <summary>
        ///     Test Return status code 404 if request not found
        /// </summary>
        /// <returns>Returns status 404</returns>
        [Fact]
        public async Task CreateOrders_Returns_Success200()
        {
            // Arrange
            var order = new OrdersViewModel();
            var newOrder = new OrderResponse { Id = new System.Guid() };

            //Mock
            this.orderService.Setup(n => n.CreateOrder(It.IsAny<OrdersViewModel>())).ReturnsAsync(newOrder);

            // Act
            var response = await this.ordersController.CreateOrders(order);

            //Assert

            Assert.NotNull(response);
            Assert.Equal((int)HttpStatusCode.OK, ((OkObjectResult)response).StatusCode);
        }
    }
}