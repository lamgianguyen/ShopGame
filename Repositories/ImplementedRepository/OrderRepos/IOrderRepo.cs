using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.GenericRepository;

namespace DUCtrongAPI.Repositories.ImplementedRepository.OrderRepos
{
    public interface IOrderRepo  : IRepository<Order>
    {
        Task<Order> CreateOrder(string userid);
        Task<List<OrderDetail>> CorfirmOrder(string orderid);
    }
}
