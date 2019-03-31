using Microsoft.AspNetCore.Mvc;

namespace seeWifi.Controllers
{
    public class ReportsController : Controller
    {
        public IActionResult SuspiciousHotSpots()
        {
            return View();
        }
    }
}