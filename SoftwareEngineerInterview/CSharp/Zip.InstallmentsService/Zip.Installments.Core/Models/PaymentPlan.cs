using System.ComponentModel.DataAnnotations;

namespace Zip.Installments.Core.Models
{
    /// <summary>
    /// Data structure which defines all the properties for a purchase installment plan.
    /// </summary>
    public class PaymentPlan
    {
        /// <summary>
        ///     Gets or sets the payment id
        /// </summary>
        [Key]
        public Guid PaymentId { get; set; }

        /// <summary>
        ///     Gets or sets the purchase amount
        /// </summary>
        public decimal PurchaseAmount { get; set; }

        /// <summary>
        ///     Gets or sets the installments
        /// </summary>
        public virtual ICollection<Installment> Installments { get; set; }

        /// <summary>
        ///     Initialize an instance of payment plan
        /// </summary>
        public PaymentPlan()
        {
            Installments = new HashSet<Installment>();
        }
    }
}
