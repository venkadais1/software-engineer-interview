using Zip.Installments.Core.Constants;

namespace Zip.Installments.Core.Models
{
    /// <summary>
    ///     Order response structure definition
    /// </summary>
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string OrderStatus { get; set; }
        public string Message { get; set; }
    }
}
