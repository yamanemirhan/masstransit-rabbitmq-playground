namespace Shared.Contracts.Events
{
    public class OrderStockCheckRequestedEvent
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string CustomerEmail { get; set; }
        public string CompanyEmail { get; set; }
        public string CustomerName { get; set; }
        public string ProductDetails { get; set; }
    }
}
