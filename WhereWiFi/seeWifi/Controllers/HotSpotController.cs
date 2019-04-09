using Microsoft.AspNetCore.Mvc;
using WiFi.Library.Models;
using WiFi.Library.Services.IServices;

namespace seeWifi.Controllers
{
    public class HotSpotController : Controller
    {
        private IHotSpotService _hotSpotService;
        public HotSpotController(IHotSpotService hotSpotService)
        {
            _hotSpotService = hotSpotService;
        }
        public IActionResult Index()
        {
            return View("HotSpotDisplay", _hotSpotService.GetAll());
        }
        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HotSpotModel hotspot)
        {
            var newHotSpo = _hotSpotService.AddHotSpot(hotspot);
            return View("HotSpotDisplay");
        }
    }
}