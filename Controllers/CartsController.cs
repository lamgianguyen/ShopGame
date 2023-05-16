using DUCtrongAPI.Models;
using DUCtrongAPI.Repositories.EmplementedRepository.CartRepos;
using DUCtrongAPI.Requests;
using DUCtrongAPI.Services.CartServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DUCtrongAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }
        //post add product to cart
        [HttpPost]
        [Authorize(Roles = "2")]
        public async Task<ActionResult<Product>> AddToCart(CartReq cartreq)
        {
            try
            {
                var cart = await _cartService.AddtoCart(cartreq);
                if (cart == null) return BadRequest();
                //return post 201 result
                return StatusCode(201, cart);
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
        public async Task<IActionResult> GetCart([FromQuery]CartPaging cartPaging)
        {
            try
            {
                var cart = await _cartService.GetCartPaging(cartPaging);
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
        //httpput update cart
        [HttpPut]
        public async Task<IActionResult> UpdateCart( UpdateCartModel updateCartModel)
        {
            try
            {
                var cart = await _cartService.UpdateCart(updateCartModel);
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
        //httpdelete remove cart
        [HttpDelete]
        public async Task<IActionResult> RemoveCart( string userid, string productid)
        {
            try
            {
                var cart = await _cartService.DeleteCart(userid, productid);
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
    }
} 
