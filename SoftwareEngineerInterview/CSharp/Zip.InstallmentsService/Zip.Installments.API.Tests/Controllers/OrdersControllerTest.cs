using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Zip.Installments.API.Controllers.v1;
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
        [Fact]
        public async Task CreateOrders_Throws_ArgumentNullExceptions()
        {
            // Arrange
            OrdersViewModel order = null;
            // Act
            var response = await this.ordersController.CreateOrders(order) as ObjectResult;

            //Assert

            Assert.NotNull(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, response?.StatusCode);

        }
    }
}