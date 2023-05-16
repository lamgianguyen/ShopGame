using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.ImplementedRepository.OrderDetailRepos;
using DUCtrongAPI.Repositories.ImplementedRepository.OrderRepos;

namespace DUCtrongAPI.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IOrderDetailRepo _orderDetailRepo;
        public OrderService(IOrderRepo orderRepo,IOrderDetailRepo orderDetailRepo)
        {
            _orderRepo = orderRepo;
            _orderDetailRepo = orderDetailRepo;
        }

        public Task<List<OrderDetail>> ConfrimOrder(string orderid)
        {
            // new list from orderrepo confirm order
            var list = _orderRepo.CorfirmOrder(orderid);
            return list;
        }

        public Task<bool> CreateOrder(string userid)
        {
            var order = _orderRepo.CreateOrder(userid);
            return order != null ? Task.FromResult(true) : Task.FromResult(false);
        }
    }
}
