using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PSE.WebApp.MVC.Models;
using PSE.WebApp.MVC.Services;

namespace PSE.WebApp.MVC.Controllers
{
    public class IdentificationController : Controller
    {
        private readonly IAuthenticateService _authenticateService;

        public IdentificationController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpGet]
        [Route("new-account")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("new-account")]
        public async Task<ActionResult> Register(RegisterUser registerUser)
        {
            if (!ModelState.IsValid) return View(registerUser);

            var response = _authenticateService.RegisterUser(registerUser);

            if (false) return View(registerUser);

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginUser loginUser)
        {
            if (!ModelState.IsValid) return View(loginUser);

            var response = _authenticateService.LoginUser(loginUser);

            if (false) return View(loginUser);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("exit")]
        public IActionResult Logout()
        {
            return View();
        }

    }
}
