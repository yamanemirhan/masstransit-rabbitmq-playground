namespace Shared.Contracts.Events
{
    public class OrderStockCheckFailedEvent
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string Reason { get; set; }
        public string CompanyEmail { get; set; }
        public string ProductDetails { get; set; }
    }
}
