using DUCtrongAPI.Models;

namespace DUCtrongAPI.Services.OrderServices
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(string userid);
        Task<List<OrderDetail>> ConfrimOrder(string orderid);
    }
}
