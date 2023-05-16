using System;
using System.Collections.Generic;

namespace DUCtrongAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public string ProductId { get; set; } = null!;
        public string? ProductName { get; set; }
        public string? ProductDetail { get; set; }
        public double? Price { get; set; }
        public int ProductTypeId { get; set; }
        public string? Img { get; set; }

        public virtual ProductType ProductType { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
