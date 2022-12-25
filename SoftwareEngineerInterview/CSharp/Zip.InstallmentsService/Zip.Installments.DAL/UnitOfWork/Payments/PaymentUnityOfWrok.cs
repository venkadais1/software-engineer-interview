using Zip.Installments.DAL.AppContext;

namespace Zip.Installments.DAL.UnitOfWork.Payments
{
    /// <summary>
    ///     Unity of framework to extend the db.
    /// </summary>
    public sealed class PaymentUnityOfWrok
    {
        private readonly OrdersDbContext ordersDb;

        /// <summary>
        ///     Initialize Unity of work framework
        /// </summary>
        /// <param name="ordersDb"></param>
        public PaymentUnityOfWrok(OrdersDbContext ordersDb)
        {
            this.ordersDb = ordersDb;
        }


    }
}
