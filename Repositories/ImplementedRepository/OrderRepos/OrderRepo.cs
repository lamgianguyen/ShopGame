using AutoMapper;
using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.GenericRepository;
using DUCtrongAPI.Repositories.ImplementedRepository.OrderDetailRepos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DUCtrongAPI.Repositories.ImplementedRepository.OrderRepos
{
    public class OrderRepo : Repository<Order>, IOrderRepo
    {
        private readonly IOrderDetailRepo _OrderDetailRepo;
        public OrderRepo(ShopingContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<OrderDetail>> CorfirmOrder(string orderid)
        {
            List<OrderDetail> listorderdetail = new List<OrderDetail>();
            var query = from order in context.Orders
                        where order.OrderId == orderid
                        select order;
            var orderToConfirm = await query.FirstOrDefaultAsync();
            if (orderToConfirm != null)
            {
                orderToConfirm.Status = true;
                await context.SaveChangesAsync();

            }
           
            List<Cart> listcart2 = await context.Carts.Where(x => x.UserId == orderToConfirm.UserId).ToListAsync();

            foreach (var item in listcart2)
            {
                //create order detail
                OrderDetail orderdetail = new OrderDetail()
                {
                    OrderDetaiId = Guid.NewGuid().ToString(),
                    OrderId = orderid,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity

                };
                
                listorderdetail.Add(orderdetail);

            }
            await context.OrderDetails.AddRangeAsync(listorderdetail);
             context.Carts.RemoveRange(listcart2);
            await context.SaveChangesAsync();
            return listorderdetail;
        }
        

        public async Task<Order> CreateOrder(string userid)
        {
            var query = from cart in context.Carts
                        where cart.UserId == userid 
                        select cart;
            var cartlist = await query.AnyAsync();
            
            Order order = new Order()
            {
                UserId = userid,
                OrderId = Guid.NewGuid().ToString(),
                Status = false
            };
            await context.Orders.AddAsync(order);
           await context.SaveChangesAsync();
            return order;

        }
    }
}
