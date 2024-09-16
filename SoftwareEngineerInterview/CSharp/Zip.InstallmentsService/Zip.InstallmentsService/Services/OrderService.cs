using FluentValidation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Zip.Installments.Core.Constants;
using Zip.Installments.Core.Interface;
using Zip.Installments.Core.Models;
using Zip.Installments.DAL.Interfaces;
using Zip.Installments.ViewModel.Orders;
using Zip.InstallmentsService.Interface;

namespace Zip.InstallmentsService.Services
{
    /// <summary>
    ///     The Implementation of order service
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly INLogger logger;
        private readonly IUnityOfWork repository;
        private readonly IValidator<OrdersViewModel> VmOrdersValidator;
        private readonly IValidator<Order> ordersValidator;

        /// <summary>
        ///     Initialize an instance of <see cref="OrderService"/>
        /// </summary>
        /// <param name="repository"></param>
        public OrderService(INLogger logger,
            IUnityOfWork repository,
            IValidator<OrdersViewModel> VmOrdersValidator,
            IValidator<Order> ordersValidator)
        {
            this.repository = repository;
            this.logger = logger;
            this.VmOrdersValidator = VmOrdersValidator;
            this.ordersValidator = ordersValidator;
        }

        /// <summary>
        ///     Get the list of orders
        /// </summary>
        /// <returns>Returns list of orders</returns>
        public async Task<IList<Order>> GetOrders()
        {
            this.logger.LogInfo($"{nameof(OrderService.GetOrders)} Fetching");
            var response = await this.repository.OrdersRepository.FindAll(x => x.Payment, y => y.Payment.Installments);

            this.logger.LogInfo($"{nameof(OrderService.GetOrders)} Completed");
            return response?.OrderByDescending(n => n.FirstName).ToList();
        }

        /// <summary>
        ///     Get the list of orders
        /// </summary>
        /// <returns>Returns list of orders</returns>
        public async Task<IList<Order>> GetOrderByFilter(
            string OrderId,
            string Email,
            string FirstName,
            string LastName,
            string OrderTitle
            )
        {
            this.logger.LogInfo($"{nameof(OrderService.GetOrders)} Fetching");
            Expression<Func<Order, bool>> filterExpression = this.GetOrderFilter(OrderId, Email, FirstName, LastName, OrderTitle);

            Expression<Func<Order, object>> orderPredicate = x => x.CreationDate;
            var response = await this.repository.OrdersRepository.FindConditoin(
                filterExpression,
                orderPredicate,
                false,
                x => x.Payment, y => y.Payment.Installments);

            this.logger.LogInfo($"{nameof(OrderService.GetOrders)} Completed");
            return response?.OrderByDescending(n => n.FirstName).ToList();
        }

        /// <summary>
        ///     Create the order of payment with installments
        /// </summary>
        /// <param name="order">An view model of order</param>
        /// <returns>Return created order</returns>
        public async Task<OrderResponse> CreateOrder(OrdersViewModel order)
        {
            this.logger.LogInfo($"{nameof(OrderService.CreateOrder)} Started");
            var orderViewModelValidation = this.VmOrdersValidator.Validate(order);
            if (orderViewModelValidation?.IsValid == false)
            {
                throw new ValidationException(orderViewModelValidation.Errors);
            }

            this.logger.LogInfo($"{nameof(OrderService.CreateOrder)} Check if already exists");
            var existingOrder = await this.repository.OrdersRepository.Find(order.Id);
            if (existingOrder != null)
            {
                throw new InvalidDataException("Invalid Order or order id");
            }

            this.logger.LogInfo($"{nameof(OrderService.CreateOrder)} Preparing payment order creation");
            var paymentId = Guid.NewGuid();
            var newOrder = new Order
            {
                Id = order.Id,
                ProductId = order.ProductId,
                Description = order.Description,
                Email = order.Email,
                FirstName = order.FirstName,
                LastName = order.LastName,
                NumberOfInstallments = order.NumberOfInstallments,
                Payment = this.CreatePaymentPlan(order, paymentId),
                CreationDate = DateTime.UtcNow
            };

            var neworderValidation = this.ordersValidator.Validate(newOrder);
            if (neworderValidation?.IsValid == false)
            {
                throw new ValidationException(neworderValidation.Errors);
            }

            this.logger.LogInfo($"{nameof(OrderService.CreateOrder)} Creating new order");
            await this.repository.OrdersRepository.Create(newOrder);
            await this.repository.Save();
            this.logger.LogInfo($"{nameof(OrderService.CreateOrder)} Order created successfully...");

            return new OrderResponse
            {
                Id = order.Id,
                Message = AppConstants.OrderCreatedSuccess,
                OrderStatus = nameof(OrderStatus.Purchased)
            };
        }

        private PaymentPlan CreatePaymentPlan(OrdersViewModel payment, Guid paymentId)
        {
            this.logger.LogInfo($"{nameof(OrderService.CreatePaymentPlan)} Creating installments");

            PaymentPlan paymentPlan = new PaymentPlan();

            if (payment == null)
            {
                throw new InvalidOperationException("Invalid payment plan");
            }

            paymentPlan.PaymentId = paymentId;
            paymentPlan.PurchaseAmount = payment.PurchaseAmount;
            paymentPlan.Installments = this.CalculateInstallments(
                payment.PurchaseAmount,
                payment.NumberOfInstallments,
                payment.Frequency,
                payment.FirstPaymentDate);

            this.logger.LogInfo($"{nameof(OrderService.CreatePaymentPlan)} End");

            return paymentPlan;
        }

        private Installment[] CalculateInstallments(decimal purchaseAmount, int numberOfInstallments, int frequency, DateTime firstPaymentDate)
        {
            this.logger.LogInfo($"{nameof(OrderService.CalculateInstallments)} Calculating Installments");

            var installments = new List<Installment>();
            var oneInstallmentAmount = purchaseAmount / numberOfInstallments;
            if (firstPaymentDate.Date < DateTime.Now.Date)
            {
                throw new InvalidOperationException("Invalid payment date");
            }
            installments.Add(new Installment
            {
                InstallmentId = Guid.NewGuid(),
                Amount = oneInstallmentAmount,
                DueDate = firstPaymentDate,
            });

            for (int term = 1; term < numberOfInstallments; term++)
            {
                installments.Add(new Installment
                {
                    InstallmentId = Guid.NewGuid(),
                    Amount = oneInstallmentAmount,
                    DueDate = firstPaymentDate.AddDays(frequency),
                });
            }

            this.logger.LogInfo($"{nameof(OrderService.CalculateInstallments)} End");
            return installments.ToArray();
        }

        private Expression<Func<Order, bool>> GetOrderFilter(string OrderId, string Email, string FirstName, string LastName, string OrderTitle)
        {
            Expression<Func<Order, bool>> filterExpression = x => true;

            if (!string.IsNullOrEmpty(OrderId))
            {
                filterExpression = x => x.Id == new Guid(OrderId);
            }
            //if (!string.IsNullOrEmpty(Email))
            //{
            //    //filterExpression = filterExpression.And(n => n.Email.Contains(Email));
            //}
            //if (!string.IsNullOrEmpty(FirstName))
            //{
            //    //filterExpression = filterExpression.And(n => n.FirstName.Contains(FirstName));
            //}
            //if (!string.IsNullOrEmpty(LastName))
            //{
            //    //filterExpression = filterExpression.And(n => n.LastName.Contains(LastName));
            //}
            //if (!string.IsNullOrEmpty(OrderTitle))
            //{
            //    //filterExpression = filterExpression.And(n => n.LastName.Contains(OrderTitle));
            //}

            return filterExpression;
        }
    }
}
