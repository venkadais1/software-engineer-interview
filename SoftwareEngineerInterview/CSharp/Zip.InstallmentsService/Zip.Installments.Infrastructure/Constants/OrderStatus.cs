
namespace Zip.Installments.Core.Constants
{
    /// <summary>
    ///     The current order status
    /// </summary>
    public enum OrderStatus
    {
        Created,
        Purchased,
        Dispatch,
        Shipped,
        Delivered,
        Finished,
        CreationFailed
    }
}
