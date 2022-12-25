using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zip.Installments.Core.Models
{
    /// <summary>
    ///     The POCO definition of orders
    /// </summary>
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid ProductId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int NumberOfInstallments { get; set; }
        public Guid PaymentId { get; set; }

        [ForeignKey(nameof(PaymentId))]
        public virtual PaymentPlan Payment { get; set; }

        public DateTime? CreationDate { get; set; }
    }
}
