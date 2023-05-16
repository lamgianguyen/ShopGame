using DUCtrongAPI.Repositories.ImplementedRepository.OrderRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DUCtrongAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [AllowAnonymous]
    public class OrdersController : Controller
    {
        private readonly IOrderRepo _OderRepo;
        public OrdersController(IOrderRepo OderRepo)
        {
            _OderRepo = OderRepo;
        }
        //post create order
        [HttpPost]
        public async Task<ActionResult> PostOrder(string userid)
        {
            try
            {
                var orderrespon = await _OderRepo.CreateOrder(userid);
                if (orderrespon == null)
                {
                    return BadRequest();
                }
                // Return a 201 response with the created product
                return StatusCode(201, orderrespon);
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
        [HttpPost("ConfirmOrder")]
        public async Task<ActionResult> ConfirmOrder(string orderid)
        {
            try
            {
                var orderrespon = await _OderRepo.CorfirmOrder(orderid);
                if (orderrespon == null)
                {
                    return BadRequest();
                }
                // Return a 201 response with the created product
                return StatusCode(201, orderrespon);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (SqlException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
