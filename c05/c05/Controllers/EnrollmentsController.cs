using Microsoft.AspNetCore.Mvc;

namespace c05.Controllers
{
    public class EnrollmentsController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}