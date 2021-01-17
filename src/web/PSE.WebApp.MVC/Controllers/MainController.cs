using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PSE.WebApp.MVC.Models;

namespace PSE.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool HasErrorsInResponse(ResponseErrorResult response)
        {
            if (response != null && response.Errors.Messages.Any()) return true;

            return false;
        }
    }
}
