using AutoMapper;
using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.EmplementedRepository.Paging;
using DUCtrongAPI.Repositories.GenericRepository;
using DUCtrongAPI.Requests;
using Microsoft.EntityFrameworkCore;

namespace DUCtrongAPI.Repositories.EmplementedRepository.CartRepos
{
    public class CartRepo : Repository<Cart>, ICartRepo
    {
        public CartRepo(ShopingContext context, IMapper mapper) : base(context, mapper)
        {
            
        }

            public async Task<Cart> GetCart(string userid, string productid)
            {
                var query = from cart in context.Carts
                            where cart.UserId == userid && cart.ProductId == productid
                            select cart;
                var value = await query.FirstOrDefaultAsync();
                return value;
            }

      
        public async Task<bool> CheckCart(string userid, string productid)
        {
            var query = from cart in context.Carts
                        where cart.UserId == userid && cart.ProductId == productid
                        select cart;
            var cartExists = await query.AnyAsync();
            return cartExists;
        }
        public async Task<bool> RemoveCart(string userid, string productid)
{
                var query = from cart in context.Carts
                where cart.UserId == userid && cart.ProductId == productid
                select cart;

                var cartToRemove = await query.FirstOrDefaultAsync();

    if (cartToRemove != null)
    {
        context.Carts.Remove(cartToRemove);
        await context.SaveChangesAsync();
        return true;
    }

    return false;
}

        public async Task<PagedResult<Cart>> GetallCartPaging(CartPaging cartpaging)
        {
            var query = from cart in context.Carts
                        where cart.UserId == cartpaging.userid
                        select cart;
            int totalRow = await query.CountAsync();

            var list = await query
                .Skip((cartpaging.pageIndex - 1) * cartpaging.pageItems)
                .Take(cartpaging.pageItems)
                .Select(selector => new Cart()
            {
                CartId=selector.CartId,
                UserId = selector.UserId,
                ProductId = selector.ProductId,
                Quantity = selector.Quantity,
            }).ToListAsync();

            var pageResult = new PagedResult<Cart>(list, totalRow, cartpaging.pageIndex, cartpaging.pageItems);

            return pageResult;
        }

        public async Task<bool> UpdateCart(UpdateCartModel updateCartModel)
        {
            if(updateCartModel.Quantity == 0)
            {
                return await RemoveCart(updateCartModel.UserId, updateCartModel.ProductId);
                
            }
            var check = await GetCart(updateCartModel.UserId, updateCartModel.ProductId);

            if (check != null)
            {
                check.Quantity = updateCartModel.Quantity;
                return true;
            }

            return false;
        }

    }
}
