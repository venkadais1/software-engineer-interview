namespace Zip.Installments.ViewModel.Orders
{
    /// <summary>
    ///     The POCO definition of orders view model
    /// </summary>
    public class OrdersViewModel
    {
        /// <summary>
        ///     Gets or sets the order id
        /// </summary>
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
        ///     Gets or sets the purchase amount
        /// </summary>
        public decimal PurchaseAmount { get; set; }

        /// <summary>
        ///     Gets or sets the payment installment count
        /// </summary>
        public int NumberOfInstallments { get; set; }

        /// <summary>
        ///     Gets or sets the payment frequency
        /// </summary>
        public int Frequency { get; set; }

        /// <summary>
        ///     Gets or sets the payment starting date
        /// </summary>
        public DateTime FirstPaymentDate { get; set; }
    }
}
