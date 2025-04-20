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
        public async Task Create(CreateOrderDto o)
        {
            await _orderService.Create(o);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(ParseOrders(await _orderService.GetAll()));
        }

        [HttpGet("/id/{uuid}")]
        public async Task<IActionResult> GetByUuid(string uuid)
        {
            return Json(ParseOrder(await _orderService.GetByUuid(uuid)));
        }

        [HttpGet("uid/{useruuid}")]
        public async Task<IActionResult> GetAllByUser(string useruuid) {
            return Json(ParseOrders(await _orderService.GetByUser(useruuid)));
        }

        [HttpGet("rid/{restaranuuid}")]
        public async Task<IActionResult> GetAllByRestaran(string restaranuuid)
        {
            return Json(ParseOrders(await _orderService.GetByRestaran(restaranuuid)));
        }
    }
}
