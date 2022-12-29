using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Zip.Installments.Core.Constants;
using Zip.Installments.Core.Interface;
using Zip.Installments.Core.Models;
using Zip.Installments.DAL.AppContext;
using Zip.Installments.DAL.Interfaces;
using Zip.Installments.ViewModel.Orders;
using Zip.InstallmentsService.Services;

namespace Zip.Installments.Service.Tests
{
    /// <summary>
    ///     The unit test case of serice layer
    /// </summary>
    public class OrderServiceTest
    {
        private readonly OrderService orderService;
        private readonly Mock<IUnityOfWork> repository;
        private readonly Mock<INLogger> logger;
        private readonly Mock<IValidator<OrdersViewModel>> VmOrdersValidator;
        private readonly Mock<IValidator<Order>> ordersValidator;
        /// <summary>
        ///     Initialize the service unit test
        /// </summary>
        public OrderServiceTest()
        {
            var dbOptions = new DbContextOptionsBuilder<OrdersDbContext>()
                                .UseInMemoryDatabase("testdb")
                                .Options;

            var dbContext = new OrdersDbContext(dbOptions);
            var rep = new OrdersRepository(dbContext);

            this.repository = new Mock<IUnityOfWork>();
            this.logger = new Mock<INLogger>();
            this.VmOrdersValidator = new Mock<IValidator<OrdersViewModel>>();
            this.ordersValidator = new Mock<IValidator<Order>>();

            this.repository.Setup(n => n.OrdersRepository).Returns(rep);
            this.orderService = new OrderService(
                this.logger.Object,
                this.repository.Object,
                this.VmOrdersValidator.Object,
                this.ordersValidator.Object);
        }

        /// <summary>
        ///     TEST: Order has valid email
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateOrder_Throws_Error_IfEmailIsInvalid()
        {
            //Arrange
            OrdersViewModel ordersViewModel = new OrdersViewModel { NumberOfInstallments = 0 };
            List<ValidationFailure> errors = new List<ValidationFailure>{ new ValidationFailure
            {
                PropertyName =nameof(OrdersViewModel.Email),
                ErrorMessage = ErrorMessage.InvalidProperty }
            };
            var validationResult = new ValidationResult { Errors = errors };

            ///Mock
            this.VmOrdersValidator.Setup(n => n.Validate(ordersViewModel)).Returns(validationResult);

            //Act
            var resp = await Record.ExceptionAsync(() => this.orderService.CreateOrder(ordersViewModel));

            //Assert
            Assert.NotNull(resp);
            Assert.IsType<ValidationException>(resp);
        }

        /// <summary>
        ///     TEST: Order conflicts
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateOrder_Throws_Error_IfOrderConclicts()
        {
            //Arrange
            OrdersViewModel ordersViewModel = new OrdersViewModel { Id = new Guid(), NumberOfInstallments = 0 };

            var validationResult = new ValidationResult { };

            ///Mock
            this.VmOrdersValidator.Setup(n => n.Validate(ordersViewModel)).Returns(validationResult);
            this.repository.Setup(n => n.OrdersRepository.Find(It.IsAny<Guid>())).ReturnsAsync(new Order());
            //Act
            var resp = await Record.ExceptionAsync(() => this.orderService.CreateOrder(ordersViewModel));

            //Assert
            Assert.NotNull(resp);
            Assert.IsType<InvalidDataException>(resp);
        }

        /// <summary>
        ///     TEST: Order payment date is invalid
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateOrder_Throws_Error_IfPaymentDateIsInvalid()
        {
            //Arrange
            var ordersViewModel = new OrdersViewModel { Id = new Guid(), NumberOfInstallments = 2, PurchaseAmount = 50M, };
            var order = new Order();

            List<ValidationFailure> errors = new List<ValidationFailure>{ new ValidationFailure
            {
                PropertyName =nameof(Order.Payment),
                ErrorMessage = ErrorMessage.InvalidProperty }
            };
            var validationResult = new ValidationResult { Errors = errors };
            var validationViewModelResultSuccess = new ValidationResult();
            Order oldOrder = null;
            ///Mock
            this.VmOrdersValidator.Setup(n => n.Validate(ordersViewModel)).Returns(validationViewModelResultSuccess);
            this.ordersValidator.Setup(n => n.Validate(order)).Returns(validationResult);
            this.repository.Setup(n => n.OrdersRepository.Find(It.IsAny<Guid>())).Returns(Task.FromResult(oldOrder));

            this.ordersValidator.Setup(n => n.Validate(order)).Returns(validationResult);

            //Act
            var resp = await Record.ExceptionAsync(() => this.orderService.CreateOrder(ordersViewModel));

            //Assert
            Assert.NotNull(resp);
            Assert.IsType<InvalidOperationException>(resp);
        }

        /// <summary>
        ///     TEST: Order payment date is invalid
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateOrder_Throws_Error_IfInstallmentsInvalid()
        {
            //Arrange
            var ordersViewModel = new OrdersViewModel
            {
                Id = new Guid(),
                NumberOfInstallments = 2,
                PurchaseAmount = 50M,
                FirstPaymentDate = DateTime.UtcNow,
            };
            var order = new Order
            {
                Id = new Guid(),
                FirstName = "someFirstName",
                LastName = "someLastName",
                CreationDate = DateTime.UtcNow,
                Email = "someEmail",
                Description = "Description",
                NumberOfInstallments = 2,
                PaymentId = new Guid(),
                Payment = new PaymentPlan { PurchaseAmount = 0 }
            };

            List<ValidationFailure> errors = new List<ValidationFailure>{ new ValidationFailure
            {
                CustomState = new object(),
                PropertyName =nameof(Order.Payment.PurchaseAmount),
                ErrorMessage = ErrorMessage.InvalidProperty }
            };
            var validationViewModelResultSuccess = new ValidationResult();
            var validationResult = new ValidationResult { Errors = errors, RuleSetsExecuted = new string[] { } };
            Order oldOrder = null;
            ///Mock
            this.VmOrdersValidator.Setup(n => n.Validate(ordersViewModel)).Returns(validationViewModelResultSuccess);

            this.repository.Setup(n => n.OrdersRepository.Find(It.IsAny<Guid>())).Returns(Task.FromResult(oldOrder));

            this.ordersValidator.Setup(n => n.Validate(It.IsAny<Order>())).Returns(validationResult);

            //Act
            var resp = await Record.ExceptionAsync(() => this.orderService.CreateOrder(ordersViewModel));

            //Assert
            Assert.NotNull(resp);
            Assert.IsType<ValidationException>(resp);
        }

        /// <summary>
        ///     TEST: Order payment date is invalid
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateOrder_Returns_Success()
        {
            //Arrange
            var ordersViewModel = new OrdersViewModel
            {
                Id = new Guid(),
                NumberOfInstallments = 2,
                PurchaseAmount = 50M,
                FirstPaymentDate = DateTime.UtcNow,
            };
            var order = new Order
            {
                Id = new Guid(),
                FirstName = "someFirstName",
                LastName = "someLastName",
                CreationDate = DateTime.UtcNow,
                Email = "someEmail",
                Description = "Description",
                NumberOfInstallments = 2,
                PaymentId = new Guid(),
                Payment = new PaymentPlan { PurchaseAmount = 0 }
            };
            
            var validationResult = new ValidationResult();
            Order oldOrder = null;
            ///Mock
            this.VmOrdersValidator.Setup(n => n.Validate(ordersViewModel)).Returns(validationResult);

            this.repository.Setup(n => n.OrdersRepository.Find(It.IsAny<Guid>())).Returns(Task.FromResult(oldOrder));

            this.ordersValidator.Setup(n => n.Validate(It.IsAny<Order>())).Returns(validationResult);

            this.repository.Setup(n => n.OrdersRepository.Create(order)).Returns(Task.FromResult(1));

            //Act
            var resp = await this.orderService.CreateOrder(ordersViewModel);

            //Assert
            Assert.NotNull(resp);
            Assert.IsType<OrderResponse>(resp);
        }
    }
}
