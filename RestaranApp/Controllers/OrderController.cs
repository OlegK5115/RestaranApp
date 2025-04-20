using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaranApp.Dto;
using RestaranApp.Entities;
using RestaranApp.Services;

namespace RestaranApp.Controllers
{
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private IOrderService _orderService;

        private Order ParseOrder(Order o)
        {
            o.Id = 0;
            o.UserId = 0;
            o.RestaranId = 0;

            return o;
        }

        private List<Order> ParseOrders(List<Order> olist)
        {
            olist.ForEach(o => {
                ParseOrder(o);
            });

            return olist;
        }

        public OrdersController(IOrderService os)
        {
            _orderService = os;
        }

        [HttpPost("create")]
        public async Task Create([FromBody] CreateOrderDto o)
        {
            await _orderService.Create(o);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(ParseOrders(await _orderService.GetAll()));
        }

        [HttpGet("{uuid}")]
        public async Task<IActionResult> GetByUuid(string uuid)
        {
            return Json(ParseOrder(await _orderService.GetByUuid(uuid)));
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetAllByUser(string username) {
            return Json(ParseOrders(await _orderService.GetByUser(username)));
        }
    }
}
