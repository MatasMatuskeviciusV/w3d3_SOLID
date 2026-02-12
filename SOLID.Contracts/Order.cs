namespace SOLID.Contracts
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
        public string? CustomerEmail { get; set; }
    }
}
