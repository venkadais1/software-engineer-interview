using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using Zip.Installments.Core.Constants;
using Zip.Installments.DAL.AppContext;

namespace Zip.Installments.DAL.Extensions
{
    /// <summary>
    ///     The DB extension class to create and add more feature for infrastructure
    /// </summary>
    public static class DbExtensions
    {
        /// <summary>
        ///     This extension for query includes for columns
        /// </summary>
        /// <typeparam name="T">T type for the extensions</typeparam>
        /// <param name="query">Extension to be added here in IQueryable</param>
        /// <param name="includes">Fields to be included</param>
        /// <returns>Returns an updated query</returns>
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query,
            params Expression<Func<T, object>>[] includes)
                 where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query;
        }

        /// <summary>
        ///     To set database instance for the application
        /// </summary>
        /// <param name="services">Add this extension in service collection</param>
        /// <param name="config">An instance of <see cref="IConfiguration"/></param>
        /// <returns>Returns services</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration config)
        {
            ////SQL DB
            services.AddDbContext<OrdersDbContext>(
                x => x.UseSqlServer(config.GetSection(ConfigConstants.DbConnection).Value));

            ////In-Memory-Db
            //services.AddDbContext<OrdersDbContext>(x => x.UseInMemoryDatabase(ConfigConstants.InMemoryDbName));

            return services;
        }
    }
}
