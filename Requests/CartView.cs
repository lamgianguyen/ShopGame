namespace DUCtrongAPI.Requests
{
    public class CartView
    {
        public string CartId { get; set; } = null!;
        public string? UserId { get; set; }
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }
        public int? Quantity { get; set; }
        public double? price { get; set; }
    }
}
