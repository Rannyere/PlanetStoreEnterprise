using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSE.Core.Mediator;
using PSE.Order.API.Application.Commands;
using PSE.Order.API.Application.Queries;
using PSE.WebAPI.Core.Controllers;
using PSE.WebAPI.Core.User;

namespace PSE.Order.API.Controllers
{
    public class OrderController : MainController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IAspNetUser _user;
        private readonly IOrderQueries _orderQueries;

        public OrderController(IMediatorHandler mediator,
                               IAspNetUser user,
                               IOrderQueries orderQueries)
        {
            _mediator = mediator;
            _user = user;
            _orderQueries = orderQueries;
        }

        [HttpPost("order")]
        public async Task<IActionResult> AddOrder(ExecuteOrderCommand order)
        {
            order.CustomerId = _user.GetUserId();
            return CustomResponse(await _mediator.SendCommand(order));
        }

        [HttpGet("order/last")]
        public async Task<IActionResult> LastOrder()
        {
            var order = await _orderQueries.GetLastOrder(_user.GetUserId());

            return order == null ? NotFound() : CustomResponse(order);
        }

        [HttpGet("order/list")]
        public async Task<IActionResult> ListByCustomer()
        {
            var orders = await _orderQueries.GetListOrdersByCustomer(_user.GetUserId());

            return orders == null ? NotFound() : CustomResponse(orders);
        }
    }
}
