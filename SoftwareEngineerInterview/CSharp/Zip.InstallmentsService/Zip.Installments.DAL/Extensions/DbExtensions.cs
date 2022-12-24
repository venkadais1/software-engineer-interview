using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using Zip.Installments.DAL.AppContext;
using Zip.Installments.Core.Constants;

namespace Zip.Installments.DAL.Extensions
{
    public static class DbExtensions
    {
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
