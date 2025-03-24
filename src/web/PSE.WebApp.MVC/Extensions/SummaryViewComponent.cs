using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PSE.WebApp.MVC.Extensions;

public class SummaryViewComponent : ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync()
    {
        return Task.FromResult<IViewComponentResult>(View());
    }
}
