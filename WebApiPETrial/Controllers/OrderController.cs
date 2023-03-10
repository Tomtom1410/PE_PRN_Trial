using BussinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPETrial.Repositories.Interface;

namespace WebApiPETrial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepositories _orderRepositories;

        public OrderController(IOrderRepositories order)
        {
            _orderRepositories = order;

        }

        [HttpGet]
        [Route("GetAllOrder")]
        public async Task<IActionResult> GetAllOrder()
        {
            var orderDetails = await _orderRepositories.GetAllOrder();
            return Ok(orderDetails);
        }

        [HttpGet]
        [Route("GetOrderByDate/{From}/{To}")]
        public async Task<IActionResult> GetByDate([FromRoute] DateTime? From, [FromRoute] DateTime? To)
        {
            var orderDetails = new List<OrderDetailsDto>();
            if (From == null || To == null) {
                orderDetails = await _orderRepositories.GetAllOrder();
                return Ok(orderDetails);
            }

            orderDetails = await _orderRepositories.GetOrderByDate(From, To);
            return Ok(orderDetails);
        }
    }
}
