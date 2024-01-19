using Microsoft.AspNetCore.Mvc;

namespace GymManager.UI.Components;

public class MainCarouselViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string priority)
    {
        // logika
        // pobranie z bazy danych
        return View();
    }
}
