using AutoMapper;
using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.GenericRepository;

namespace DUCtrongAPI.Repositories.ImplementedRepository.OrderDetailRepos
{
    public class OrderDetailRepo : Repository<OrderDetail>, IOrderDetailRepo
    {
        public OrderDetailRepo(ShopingContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
