using System;
using Microsoft.AspNetCore.Mvc;
using PSE.WebApp.MVC.Models;

namespace PSE.WebApp.MVC.Extensions
{
    public class PaginationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPagedList modelPaged)
        {
            return View(modelPaged);
        }
    }
}
