namespace DUCtrongAPI.Requests
{
    public class OrderDetailView
    {
        public string OrderDetaiId { get; set; } = null!;
        public string? OrderId { get; set; }
        public string? ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public double? price { get; set; }
        public string productTypeName { get; set; }
        public int? Quantity { get; set; }
    }
}
