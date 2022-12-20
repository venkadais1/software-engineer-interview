using Zip.Installments.DAL.AppContext;
using Zip.Installments.DAL.Interfaces;

namespace Zip.Installments.DAL.UnitOfWork.Payments
{
    public sealed class PaymentUnityOfWrok : IPaymentUnityOfWrok
    {
        private readonly OrdersDbContext ordersDb;

        public PaymentUnityOfWrok(OrdersDbContext ordersDb)
        {
            this.ordersDb = ordersDb;
        }


    }
}
