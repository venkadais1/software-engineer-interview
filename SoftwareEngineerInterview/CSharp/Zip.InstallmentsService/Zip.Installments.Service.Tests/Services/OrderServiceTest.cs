using FluentValidation;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Zip.Installments.Core.Models;
using Zip.Installments.DAL.Interfaces;
using Zip.Installments.Validations.Controllers;
using Zip.Installments.Validations.Services;
using Zip.Installments.ViewModel.Orders;
using Zip.InstallmentsService.Interface;
using Zip.InstallmentsService.Services;

namespace Zip.Installments.Service.Tests
{
    /// <summary>
    ///     The unit test case of serice layer
    /// </summary>
    public class OrderServiceTest
    {
        private readonly OrderService orderService;
        private readonly Mock<IRepositoryWrapper> repository;
        private readonly Mock<INLogger> logger;
        private readonly Mock<IValidator<OrdersViewModel>> VmOrdersValidator;
        private readonly Mock<IValidator<Order>> ordersValidator;
        /// <summary>
        ///     Initialize the service unit test
        /// </summary>
        public OrderServiceTest()
        {
            this.repository = new Mock<IRepositoryWrapper>();
            this.logger = new Mock<INLogger>();
            this.VmOrdersValidator = new Mock<IValidator<OrdersViewModel>>();
            this.ordersValidator = new Mock<IValidator<Order>>();
            this.orderService = new OrderService(
                this.logger.Object,
                this.repository.Object,
                this.VmOrdersValidator.Object,
                this.ordersValidator.Object);
        }

        /// <summary>
        ///     TEST: Order has valid installment
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateOrder_Throws_ArgumentException_IfInsallmentInvalid()
        {
            //Arrange
            OrdersViewModel ordersViewModel = new OrdersViewModel { NumberOfInstallments = 0 };
            //Act
            var resp = await Record.ExceptionAsync(() => this.orderService.CreateOrder(ordersViewModel));

            //Assert
            Assert.NotNull(resp);
            Assert.IsType<ArgumentNullException>(resp);
        }
    }
}
