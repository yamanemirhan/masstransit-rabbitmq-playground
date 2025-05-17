namespace OrderService.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CompanyEmail { get; set; }
        public string ProductDetails { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; } = "Pending";
        public string Message { get; set; } = "Sipariş işleniyor";
    }
}
