using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Zip.Installments.Infrastructure.Constants;
using Zip.Installments.DAL.Interfaces;
using Zip.Installments.Infrastructure.Models;
using Zip.Installments.ViewModel.Orders;
using Zip.InstallmentsService.Interface;

namespace Zip.InstallmentsService.Services
{
    /// <summary>
    ///     The Implemetation of order service
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IRepositoryWrapper repository;
        private readonly INLogger logger;

        /// <summary>
        ///     Initialize an instance of <see cref="OrderService"/>
        /// </summary>
        /// <param name="repository"></param>
        public OrderService(IRepositoryWrapper repository, INLogger logger)
        {
            this.repository = repository;
            this.logger = logger;
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
        ///     Create the order of payment with instalments
        /// </summary>
        /// <param name="order">An view model of order</param>
        /// <returns>Return created order</returns>
        public async Task<OrderResponse> CreateOrder(OrdersViewModel order)
        {
            this.logger.LogInfo($"{nameof(OrderService.CreateOrder)} Started");
            if (order == null || order.NumberOfInstallments <= 0)
            {
                throw new ArgumentNullException("Invalid Instalments");
            }

            this.logger.LogInfo($"{nameof(OrderService.CreateOrder)} Check if already exists");
            var existingOrder = await this.repository.OrdersRepository.FindConditoin(x => x.Id == order.Id);
            if (existingOrder != null && existingOrder.Any())
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
                Payment = this.CreateInstallments(order, paymentId)
            };

            this.logger.LogInfo($"{nameof(OrderService.CreateOrder)} Creating new order");
            await this.repository.OrdersRepository.Create(newOrder);
            await this.repository.Save();
            this.logger.LogInfo($"{nameof(OrderService.CreateOrder)} Order created successfully...");

            return new OrderResponse
            {
                Id = order.Id,
                Message = AppConstants.OrderCreatedSuccess,
                OrderStatus = OrderStatus.Purchased
            };
        }

        private PaymentPlan CreateInstallments(OrdersViewModel payment, Guid paymentId)
        {
            this.logger.LogInfo($"{nameof(OrderService.CreateInstallments)} Creating installments");

            PaymentPlan paymentPlan = new PaymentPlan();

            if (payment == null)
            {
                throw new InvalidOperationException("Invalid payment plan");
            }

            if (payment.NumberOfInstallments == 0)
            {
                throw new InvalidOperationException("No valid installments found");
            }
            paymentPlan.PaymentId = paymentId;
            paymentPlan.PurchaseAmount = payment.PurchaseAmount;
            paymentPlan.Installments = this.CalculateInstallments(
                payment.PurchaseAmount,
                payment.NumberOfInstallments,
                payment.Frequency,
                payment.FirstPaymentDate);

            this.logger.LogInfo($"{nameof(OrderService.CreateInstallments)} End");

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
    }
}
