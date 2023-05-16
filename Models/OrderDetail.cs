using System;
using System.Collections.Generic;

namespace DUCtrongAPI.Models
{
    public partial class OrderDetail
    {
        public string OrderDetaiId { get; set; } = null!;
        public string? OrderId { get; set; }
        public string? ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
