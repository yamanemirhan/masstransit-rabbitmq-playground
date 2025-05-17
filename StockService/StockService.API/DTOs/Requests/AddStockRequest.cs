namespace StockService.API.DTOs.Requests
{
    public class AddStockRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
