using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zip.Installments.Core.Models
{
    /// <summary>
    ///     The POCO definition of orders
    /// </summary>
    public class Order
    {
        /// <summary>
        ///     Gets or sets the order id
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the order description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the product id
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        ///     Gets or sets the user first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets or sets the user last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Gets or sets the user email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the payment installment count
        /// </summary>
        public int NumberOfInstallments { get; set; }

        /// <summary>
        ///     Gets or sets the payment id
        /// </summary>
        public Guid PaymentId { get; set; }

        /// <summary>
        ///     Gets or sets the payment plan
        /// </summary>
        [ForeignKey(nameof(PaymentId))]
        public virtual PaymentPlan Payment { get; set; }

        /// <summary>
        ///     Gets or sets the order creation date
        /// </summary>
        public DateTime? CreationDate { get; set; }
    }
}
