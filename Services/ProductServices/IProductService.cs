using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.EmplementedRepository.Paging;
using DUCtrongAPI.Repositories.GenericRepository;
using DUCtrongAPI.Requests;
using Microsoft.AspNetCore.Http;

namespace DUCtrongAPI.Services.ProductServices
{
    public interface IProductService 
    {
        Task<bool> Add(ProductReq productreq, IFormFile file);
        Task<PagedResult<ProductViewPaging>> GetProductPaging(ProductPaging productPaging);
        Task<ProductViewPaging> GetProductbyId(string id);
    }
}
