using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
