using DUCtrongAPI.Models;

namespace DUCtrongAPI.Requests
{
    public class OrderViewDetail
    {
        public string OrderId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public bool? Status { get; set; }
        public List<OrderDetailView> list { get; set; }
    }
}
