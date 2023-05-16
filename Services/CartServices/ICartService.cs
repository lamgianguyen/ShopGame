using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.EmplementedRepository.Paging;
using DUCtrongAPI.Requests;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DUCtrongAPI.Services.CartServices
{
    public interface ICartService 
    {
        Task<bool> AddtoCart(CartReq cartreq);
        Task<bool> UpdateCart(UpdateCartModel updateCartModel);
        Task<bool> DeleteCart(string userid, string productid);
        Task<PagedResult<Cart>> GetCartPaging(CartPaging cartpaging);
    }
}
