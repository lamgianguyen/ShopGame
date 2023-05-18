using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.EmplementedRepository.CartRepos;
using DUCtrongAPI.Repositories.EmplementedRepository.Paging;
using DUCtrongAPI.Requests;
using DUCtrongAPI.Utilities.commons;
using DUCtrongAPI.Utilities;

namespace DUCtrongAPI.Services.CartServices
{
    public class CartSevice : ICartService
    {
        private readonly ICartRepo _cartRepo;
        private readonly DecodeToken _decodeToken;
        public CartSevice(ICartRepo cartRepo)
        {
            _cartRepo = cartRepo;
            _decodeToken = new DecodeToken();
        }
        public async Task<bool> AddtoCart(CartReq cartreq)
        {
           
           

            var check =  await _cartRepo.GetCartItem(cartreq.userid, cartreq.productid);
            if (check !=null)
            {
                check.Quantity = cartreq.quantity;
                await _cartRepo.Update();
                return true;
            }
            Cart addcart = new Cart()
            {
                
                CartId = Guid.NewGuid().ToString(),
                UserId = cartreq.userid,
                ProductId = cartreq.productid,
                Quantity = 1
            };
            await _cartRepo.Insert(addcart);
            return true;
        }

        public Task<bool> DeleteCart(string userid, string productid)
        {
            //delete cart
            var deletecart = _cartRepo.RemoveCart(userid, productid);
            return deletecart;
        }

        public Task<PagedResult<CartView>> GetCartPaging(CartPaging cartpaging)
        {
            var listcart = _cartRepo.GetallCartPaging(cartpaging);
            return listcart;
        }

        public Task<bool> UpdateCart(UpdateCartModel updateCartModel)
        {
            var updatecart = _cartRepo.UpdateCart(updateCartModel);
            return updatecart;
        }
        
    }
}
