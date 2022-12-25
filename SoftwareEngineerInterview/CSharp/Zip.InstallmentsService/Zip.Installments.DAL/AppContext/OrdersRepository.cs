﻿using Zip.Installments.Core.Models;
using Zip.Installments.DAL.Interfaces;

namespace Zip.Installments.DAL.AppContext
{
    /// <summary>
    ///     The order repository
    /// </summary>
    public class OrdersRepository : RepositoryBase<Order>, IOrdersRepository
    {
        /// <summary>
        ///     Initialize an db context instance
        /// </summary>
        /// <param name="dbContext">An instance of db context</param>
        public OrdersRepository(OrdersDbContext dbContext)
            : base(dbContext)
        {
        }

    }
}
