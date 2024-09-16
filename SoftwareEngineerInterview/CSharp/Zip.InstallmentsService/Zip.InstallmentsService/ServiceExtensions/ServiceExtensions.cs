using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Zip.Installments.Core.Models;
using Zip.Installments.DAL.AppContext;
using Zip.Installments.DAL.Interfaces;
using Zip.Installments.Validations.Controllers;
using Zip.Installments.Validations.Services;
using Zip.Installments.ViewModel.Orders;
using Zip.InstallmentsService.Interface;
using Zip.InstallmentsService.Services;

namespace Zip.InstallmentsService.ServiceExtensions
{
    /// <summary>
    ///     The service extension for object scopes
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        ///     An service extensions
        /// </summary>
        /// <param name="service">An instance of services</param>
        /// <returns>Returns services</returns>
        public static IServiceCollection AddServiceExtensions(this IServiceCollection service)
        {
            service.AddScoped<IOrderService, OrderService>();
            service.AddScoped<IUnityOfWork, UnitOfWork>();
            service.AddScoped<IOrdersRepository, OrdersRepository>();
            service.AddScoped<IValidator<OrdersViewModel>, OrdersViewModelValidator>();
            service.AddScoped<IValidator<Order>, OrderValidator>();

            return service;
        }

    }
}
