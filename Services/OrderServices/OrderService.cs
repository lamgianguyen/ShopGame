using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.EmplementedRepository.Paging;
using DUCtrongAPI.Repositories.ImplementedRepository.OrderDetailRepos;
using DUCtrongAPI.Repositories.ImplementedRepository.OrderRepos;
using DUCtrongAPI.Requests;

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

        public Task<bool> ConfrimOrder(string orderid,bool check)
        {
            // new list from orderrepo confirm order
            var list = _orderRepo.CorfirmOrder(orderid,check);
            return list;
        }

        public Task<bool> CreateOrder(string userid)
        {
            var order = _orderRepo.CreateOrder(userid);
            return order != null ? Task.FromResult(true) : Task.FromResult(false);
        }

        public async Task<PagedResult<OrderViewPaging>> GetAllOrder(PagingRequestBase pagingRequestBase)
        {
            var result = await _orderRepo.GetOrderPaging(pagingRequestBase);
            return result;

            
        }
    }
}
