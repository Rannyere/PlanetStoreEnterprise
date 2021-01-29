using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSE.WebAPI.Core.Controllers;

namespace PSE.Clients.API.Controllers
{
    public class CustomerController : MainController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
