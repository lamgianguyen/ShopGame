using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.EmplementedRepository.Paging;
using DUCtrongAPI.Repositories.GenericRepository;
using DUCtrongAPI.Requests;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DUCtrongAPI.Repositories.ImplementedRepository.OrderRepos
{
    public interface IOrderRepo  : IRepository<Order>
    {
        Task<bool> CreateOrder(string userid);
        Task<bool> CorfirmOrder(string orderid, bool check);
        Task<PagedResult<OrderViewPaging>> GetOrderPaging(PagingRequestBase pagingRequestBase);
        Task<OrderViewDetail> GetOrderId(string id);
    }
}
