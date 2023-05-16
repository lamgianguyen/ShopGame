using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.EmplementedRepository.Paging;
using DUCtrongAPI.Repositories.GenericRepository;
using DUCtrongAPI.Requests;
using System.Threading.Tasks;

namespace DUCtrongAPI.Repositories.EmplementedRepository.ProductRepos
{
    public interface IProductRepo : IRepository<Product>
    {
        Task<PagedResult<ProductViewPaging>> GetallProductPaging(ProductPaging productPaging);
    }
}
