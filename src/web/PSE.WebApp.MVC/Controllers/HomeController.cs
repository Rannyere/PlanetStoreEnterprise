﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PSE.WebApp.MVC.Models;

namespace PSE.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("error/{id:length(3,3)}")]
        public IActionResult Errors(int id)
        {
            var modelError = new ErrorViewModel();

            if (id == 500)
            {
                modelError.Message = "An error has occurred! Please try again later or contact our support.";
                modelError.Title = "An error has occurred!";
                modelError.ErroCode = id;
            }
            else if (id == 404)
            {
                modelError.Message = "The page you are looking for does not exist! <br /> If you have any questions please contact our support";
                modelError.Title = "Ops! Page not found!";
                modelError.ErroCode = id;
            }
            else if (id == 403)
            {
                modelError.Message = "You are not allowed to do this.";
                modelError.Title = "Access denied";
                modelError.ErroCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", modelError);
        }
    }
}
