using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.EmplementedRepository.Paging;
using DUCtrongAPI.Requests;

namespace DUCtrongAPI.Services.OrderServices
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(string userid);
        Task<bool> ConfrimOrder(string orderid,bool check);
        Task<PagedResult<OrderViewPaging>> GetAllOrder(PagingRequestBase pagingRequestBase);
    }
}
