using AutoMapper;
using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.EmplementedRepository.Paging;
using DUCtrongAPI.Repositories.EmplementedRepository.UserRepos;
using DUCtrongAPI.Repositories.GenericRepository;
using DUCtrongAPI.Requests;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DUCtrongAPI.Repositories.EmplementedRepository.ProductRepos
{
    public class ProductRepo : Repository<Product>, IProductRepo
    {
        public ProductRepo(ShopingContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<PagedResult<ProductViewPaging>> GetallProductPaging(ProductPaging productPaging)
        {
            var query = from p in context.Products
                        join pt in context.ProductTypes on p.ProductTypeId equals pt.ProductTypeId
                        select new
                        {
                            p.ProductId,
                            p.ProductName,
                            p.ProductDetail,
                            p.Price,
                            ProductTypeName = pt.ProductTypeName,
                            p.Img
                        };
            if (!string.IsNullOrEmpty(productPaging.ProductName))
            {
                query = query.Where(x => x.ProductName.Contains(productPaging.ProductName));
            }
            var totalRow = await query.CountAsync();
            var list = await query
               .Skip((productPaging.pageIndex - 1) * productPaging.pageItems)
               .Take(productPaging.pageItems)
               .Select(selector => new ProductViewPaging()
               {
                 ProductId = selector.ProductId,
                 ProductName = selector.ProductName,
                 ProductDetail = selector.ProductDetail,
                 ProductTypeName = selector.ProductTypeName,
                 Price = selector.Price,
                 Img = selector.Img
                 
               }).ToListAsync();

            var pageResult = new PagedResult<ProductViewPaging>(list, totalRow, productPaging.pageIndex, productPaging.pageItems);

            return pageResult;
        }
        //create insert product from product input


    }
}
