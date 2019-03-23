using Microsoft.AspNetCore.Mvc;
using seeWifi.Interfaces;
using seeWifi.Models;

namespace seeWifi.Controllers
{
    public class AddHotSpotController : Controller
    {
        private IHotSpotService _hotSpotService;
        public AddHotSpotController(IHotSpotService hotSpotService)
        {
            _hotSpotService = hotSpotService;
        }
        public IActionResult Index()
        {
            return View("Index", _hotSpotService.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HotSpotModel hotspot)
        {
            var newHotSpo = _hotSpotService.AddHotSpot(hotspot);
            return View("Create");
        }
    }
}