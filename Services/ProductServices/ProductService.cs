using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.EmplementedRepository.Paging;
using DUCtrongAPI.Repositories.EmplementedRepository.ProductRepos;
using DUCtrongAPI.Requests;

namespace DUCtrongAPI.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;

        public ProductService(IProductRepo productRepo)
        {
            this._productRepo = productRepo;
        }

        public async Task<bool> Add(ProductReq productReq)
        {
            try
            {
                //create product from productreq
                var product = new Product()
                {
                    ProductId = Guid.NewGuid().ToString(),
                    ProductName = productReq.ProductName,
                    Price = productReq.Price,
                    ProductDetail = productReq.ProductDetail,
                    Img = productReq.Img,
                    ProductTypeId = productReq.ProductTypeId,
                };
                return await _productRepo.Insert(product);
            }
            catch (Exception ex)
            {
                // Handle the exception here or re-throw it to be caught higher up the call stack.
                return false;
            }
        }

        public Task<PagedResult<ProductViewPaging>> GetProductPaging(ProductPaging productPaging)
        {
            var listProduct = _productRepo.GetallProductPaging(productPaging);
            return listProduct;
        }
    }
}
