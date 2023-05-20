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

            public async Task<Cart> GetCartItem(string userid, string productid)
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

        public async Task<PagedResult<CartView>> GetallCartPaging(CartPaging cartpaging)
        {
            var query = from cart in context.Carts
                        where cart.UserId == cartpaging.userid 
                        join p in context.Products on cart.ProductId equals p.ProductId
                        select new { cart,p };
                       
            int totalRow = await query.CountAsync();

            var list = await query
                .Skip((cartpaging.pageIndex - 1) * cartpaging.pageItems)
                .Take(cartpaging.pageItems)
                .Select(selector => new CartView()
                {
                    ProductId = selector.cart.ProductId,
                    ProductName = selector.p.ProductName,
                    ProductImage = selector.p.Img,
                    Quantity = selector.cart.Quantity,
                    UserId = selector.cart.UserId,
                    CartId = selector.cart.CartId,
                    price = selector.p.Price
                }).OrderBy(cartView => cartView.ProductName) // Order by ProductName
    .ToListAsync();

            var pageResult = new PagedResult<CartView>(list, totalRow, cartpaging.pageIndex, cartpaging.pageItems);

            return pageResult;
        }

        public async Task<bool> UpdateCart(UpdateCartModel updateCartModel)
        {
            if(updateCartModel.Quantity == 0)
            {
                return await RemoveCart(updateCartModel.UserId, updateCartModel.ProductId);
                
            }
            var check = await GetCartItem(updateCartModel.UserId, updateCartModel.ProductId);

            if (check != null)
            {
                check.Quantity = updateCartModel.Quantity;
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

    }
}
