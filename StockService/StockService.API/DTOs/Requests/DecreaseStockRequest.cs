namespace StockService.API.DTOs.Requests
{
    public class DecreaseStockRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
