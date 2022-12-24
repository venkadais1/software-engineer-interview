using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Zip.Installments.DAL.AppContext;
using Zip.Installments.DAL.Interfaces;
using Zip.Installments.Infrastructure.Models;
using Zip.Installments.Validations.Controllers;
using Zip.Installments.Validations.Services;
using Zip.Installments.ViewModel.Orders;
using Zip.InstallmentsService.Interface;
using Zip.InstallmentsService.Services;

namespace Zip.InstallmentsService.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServiceExtensions(this IServiceCollection service)
        {
            service.AddScoped<IOrderService, OrderService>();
            service.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            service.AddScoped<IOrdersRepository, OrdersRepository>();
            service.AddTransient<IValidator<OrdersViewModel>, OrdersViewModelValidator>();
            service.AddTransient<IValidator<Order>, OrderValidator>();
            //        service.AddControllers()
            //.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<OrdersViewModelValidator>());

            return service;
        }

    }
}
