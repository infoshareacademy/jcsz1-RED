using Microsoft.AspNetCore.Mvc;
using seeWifi.Interfaces;

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
    }
}