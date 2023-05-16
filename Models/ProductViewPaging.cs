namespace DUCtrongAPI.Models
{
    public class ProductViewPaging
    {
        //extend from product models
        public string ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDetail { get; set; }
        public double? Price { get; set; }
        public string ProductTypeName { get; set; }
        public string? Img { get; set; }
        
    }
}
