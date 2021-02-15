using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSE.Clients.API.Application.Commands;
using PSE.Core.Mediator;
using PSE.WebAPI.Core.Controllers;

namespace PSE.Clients.API.Controllers
{
    public class CustomerController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CustomerController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("clients")]
        public async Task<IActionResult> Index()
        {
            //Test
            var result = await _mediatorHandler
                .SendCommand(new CustomerRegisterCommand(Guid.NewGuid(), "Rannyere", "rannyere@teste.com", "92387928016"));

            return CustomResponse(result);
        }
    }
}
