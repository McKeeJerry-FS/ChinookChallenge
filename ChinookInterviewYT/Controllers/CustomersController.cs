using ChinookInterviewYT.Client.Models;
using ChinookInterviewYT.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChinookInterviewYT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(ChinookContext chinookContext) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetCustomersASync()
        {
            try
            {
                var customers = await chinookContext.Customers.ToListAsync();
                return Ok(customers);
            }
            catch (Exception ex) {
                return Problem();
            }


            
        }
    }
}
