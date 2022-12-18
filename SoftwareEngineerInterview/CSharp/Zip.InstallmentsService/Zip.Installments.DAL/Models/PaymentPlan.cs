using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zip.Installments.DAL.Models
{
    /// <summary>
    /// Data structure which defines all the properties for a purchase installment plan.
    /// </summary>
    public class PaymentPlan
    {
        [Key]
        public Guid PaymentId { get; set; }

		public decimal PurchaseAmount { get; set; }

        public virtual ICollection<Installment> Installments { get; set; }

        public PaymentPlan()
        {
            Installments = new HashSet<Installment>();
        }
    }
}
