using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.EmplementedRepository.Paging;
using DUCtrongAPI.Repositories.EmplementedRepository.ProductRepos;
using DUCtrongAPI.Requests;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;

namespace DUCtrongAPI.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly Cloudinary _cloudinary;
        
        public ProductService(IProductRepo productRepo, IConfiguration configuration)
        {
            this._productRepo = productRepo;
            var cloudinaryAccount = configuration.GetSection("CloudinaryAccount");
            var cloudName = cloudinaryAccount["CloudName"];
            var apiKey = cloudinaryAccount["ApiKey"];
            var apiSecret = cloudinaryAccount["ApiSecret"];

            // Khởi tạo đối tượng Cloudinary
            _cloudinary = new Cloudinary(new Account(cloudName, apiKey, apiSecret));
        }

        public async Task<bool> Add(ProductReq productReq, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    //return ActionResult("No file uploaded.");
                    return false;

                }

                // Tạo đối tượng UploadParams để thiết lập các tham số upload
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    PublicId = $"your_folder_name/{Guid.NewGuid()}", // Đặt tên cho ảnh
                    Overwrite = true // Ghi đè nếu ảnh đã tồn tại
                };

                // Gửi yêu cầu upload
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                // Trả về URL của ảnh đã upload
                var imageUrl = uploadResult.SecureUrl.ToString();
                
                //create product from productreq
                var product = new Product()
                {
                    ProductId = Guid.NewGuid().ToString(),
                    ProductName = productReq.ProductName,
                    Price = productReq.Price,
                    ProductDetail = productReq.ProductDetail,
                    Img = imageUrl,
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

        public async Task<ProductViewPaging> GetProductbyId(string id)
        {
            ProductViewPaging product =await _productRepo.GetProduct(id);
            return product;
        }

        public Task<PagedResult<ProductViewPaging>> GetProductPaging(ProductPaging productPaging)
        {
            var listProduct = _productRepo.GetallProductPaging(productPaging);
            return listProduct;
        }
    }
}
