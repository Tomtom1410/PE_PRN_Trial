using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPETrial.Repositories.Interface;

namespace WebApiPETrial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IOrderRepositories _orderRepositories;

        public CustomerController(IOrderRepositories order)
        {
            _orderRepositories = order;

        }

        [HttpPost]
        [Route("Delete/{CustomerId}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute]string CustomerId)
        {
            var customer = await _orderRepositories.GetCustomerById(CustomerId);
            if (customer == null)
            {
                return NotFound();
            }

            var result = await _orderRepositories.DeleteCustomer(customer);

            if(result == null)
            {
                return Conflict("There was an unknown error when performing data deletion.");
            }
            return Ok(result);
        }
    }
}
