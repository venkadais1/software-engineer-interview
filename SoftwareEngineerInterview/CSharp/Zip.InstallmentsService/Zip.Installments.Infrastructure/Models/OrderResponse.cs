using Zip.Installments.Infrastructure.Constants;

namespace Zip.Installments.Infrastructure.Models
{
    /// <summary>
    ///     Order response structure definition
    /// </summary>
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string Message { get; set; }
    }
}
