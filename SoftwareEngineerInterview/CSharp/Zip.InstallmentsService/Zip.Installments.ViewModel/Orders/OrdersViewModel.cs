namespace Zip.Installments.ViewModel.Orders
{
    public class OrdersViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid ProductId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal PurchaseAmount { get; set; }
        public int NumberOfInstallments { get; set; }
        public int Frequency { get; set; }
        public DateTime FirstPaymentDate { get; set; }
    }
}
