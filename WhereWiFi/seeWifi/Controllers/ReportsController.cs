using Microsoft.AspNetCore.Mvc;
using WiFi.Library.Services.IServices;

namespace seeWifi.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportsService _reportsService;

        public ReportsController(IReportsService reportsService)
        {
            _reportsService = reportsService; 
        }

        public IActionResult Index()
        {
            var model = _reportsService.GetSuspiciousHotSpotsList();

            return View(model);
        }

        public IActionResult LowestCurrentHotSpotUsers()
        {
            var model = _reportsService.GetLowestCurrentHotSpotUsers();

            return View(model);
        }
    }
}