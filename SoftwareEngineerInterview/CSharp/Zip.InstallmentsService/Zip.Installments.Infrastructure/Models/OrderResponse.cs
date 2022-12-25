namespace Zip.Installments.Core.Models
{
    /// <summary>
    ///     Order response structure definition
    /// </summary>
    public class OrderResponse
    {
        /// <summary>
        ///     Gets or sets the order id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the order current status
        /// </summary>
        public string OrderStatus { get; set; }

        /// <summary>
        ///     Gets or sets the response message
        /// </summary>
        public string Message { get; set; }
    }
}
