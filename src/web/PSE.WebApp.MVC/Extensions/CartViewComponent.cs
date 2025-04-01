using Microsoft.AspNetCore.Mvc;
using PSE.WebApp.MVC.Services.Interfaces;
using System.Threading.Tasks;

namespace PSE.WebApp.MVC.Extensions;

public class CartViewComponent : ViewComponent
{
    private readonly ISalesBffService _salesService;

    public CartViewComponent(ISalesBffService salesService)
    {
        _salesService = salesService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(await _salesService.GetQuantityProductsInCart());
    }
}
