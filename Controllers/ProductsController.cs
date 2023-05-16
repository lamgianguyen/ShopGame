using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.EmplementedRepository.ProductRepos;
using DUCtrongAPI.Requests;
using Microsoft.Data.SqlClient;
using DUCtrongAPI.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;

namespace DUCtrongAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [AllowAnonymous]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostProduct(ProductReq productReq)
        {
            //create productreq from input
           
            try
            {
                var productrespon = await _productService.Add(productReq);
                if (productrespon == null)
                {
                    return BadRequest();
                }
                // Return a 201 response with the created product
                return StatusCode(201, productrespon);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Internal server exception");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal server exception");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetProduct([FromQuery] ProductPaging ProductPaging)
        {
            try
            {
                var cart = await _productService.GetProductPaging(ProductPaging);
                if (cart == null) return BadRequest();
                //return post 201 result
                return StatusCode(200, cart);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Internal server exception");
            }
            catch (SqlException)
            {
                return StatusCode(500, "Internal server exception");
            }

        }

        //return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
    }

}  

