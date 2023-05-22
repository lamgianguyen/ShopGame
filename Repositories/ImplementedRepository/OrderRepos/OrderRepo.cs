using AutoMapper;
using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.EmplementedRepository.Paging;
using DUCtrongAPI.Repositories.GenericRepository;
using DUCtrongAPI.Repositories.ImplementedRepository.OrderDetailRepos;
using DUCtrongAPI.Requests;
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

        public async Task<bool> CorfirmOrder(string orderid,bool check)
        {
           
            var query = from order in context.Orders
                        where order.OrderId == orderid
                        select order;
            var orderToConfirm = await query.FirstOrDefaultAsync();
            if (orderToConfirm != null && check == true)
            {
                orderToConfirm.Status = true;
                await context.SaveChangesAsync();
                return true;
            }
            else if (orderToConfirm != null && check == false)
            {
                orderToConfirm.Status = false;
                var query2 = from order in context.Orders
                             where order.OrderId == orderid                         
                             select order;
                var query3 = from orderdetail in context.OrderDetails
                             where orderdetail.OrderId == orderid
                             select orderdetail;
                //excute remove orderdetail
                var orderremove = await query2.ToListAsync();
                var listorderdetail = await query3.ToListAsync();
                context.Orders.RemoveRange(orderremove);
                context.OrderDetails.RemoveRange(listorderdetail);
                await context.SaveChangesAsync();
                return true;
            }


            return false;

        }
        

        public async Task<bool> CreateOrder(string userid)
        {
            var query = from cart in context.Carts
                        where cart.UserId == userid 
                        select cart;
            List<Cart> cartlist = await query.ToListAsync();
            
            Order order = new Order()
            {
                UserId = userid,
                OrderId = Guid.NewGuid().ToString(),
                Status = null
            };
            await context.Orders.AddAsync(order);
            List<OrderDetail> listorderdetail = new List<OrderDetail>();
            var query2 = from o in context.Orders
                        where o.OrderId == order.OrderId
                        select o;
            var orderToConfirm = await query2.FirstOrDefaultAsync();
         

            

            foreach (var item in cartlist)
            {
                //create order detail
                OrderDetail orderdetail = new OrderDetail()
                {
                    OrderDetaiId = Guid.NewGuid().ToString(),
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity

                };

                listorderdetail.Add(orderdetail);

            }
            await context.OrderDetails.AddRangeAsync(listorderdetail);
            context.Carts.RemoveRange(cartlist);
            await context.SaveChangesAsync();
            return true;
            

        }

        public async Task<OrderViewDetail> GetOrderId(string id)
        {
            var query = from o in context.Orders
                        where o.OrderId == id
                        select o;

           var result = await query.FirstOrDefaultAsync();
            var query2 = from od in context.OrderDetails
                         where od.OrderId == id
                         select new OrderDetailView()
                         {
                             OrderDetaiId = od.OrderDetaiId,
                             OrderId = od.OrderId,
                             ProductId = od.ProductId,
                             Quantity = od.Quantity
                         };
            var list2 = await query2.ToListAsync();
            var order = new OrderViewDetail()
            {

                OrderId = result.OrderId,
                UserId = result.UserId,
                Status = result.Status,
                list = list2
            };
            return order;
        }

        public async Task<PagedResult<OrderViewPaging>> GetOrderPaging(PagingRequestBase pagingRequestBase)
        {
            var query = from o in context.Orders
                        join u in context.Users on o.UserId equals u.UserId
                        select new OrderViewPaging()
                        {
                            orderid = o.OrderId,
                            userid = o.UserId,
                            status = o.Status,
                            username=u.UserName,
                            address= u.Address,
                            phonenumber = u.PhoneNumber
                        };
            var list2 = await query.
                Skip((pagingRequestBase.pageIndex - 1) * pagingRequestBase.pageItems)
               .Take(pagingRequestBase.pageItems).ToListAsync();
            /*var list = await context.Orders
               .Skip((pagingRequestBase.pageIndex - 1) * pagingRequestBase.pageItems)
               .Take(pagingRequestBase.pageItems)
               .Select(selector => new OrderViewPaging()
               {

                   orderid = selector.OrderId,
                   userid = selector.UserId,
                   status = selector.Status,
               }).ToListAsync(); */
            var totalRow = await query.CountAsync();
            

            var pageResult = new PagedResult<OrderViewPaging>(list2, totalRow, pagingRequestBase.pageIndex, pagingRequestBase.pageItems);

            return pageResult;
        }
    }
}
