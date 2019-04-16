using System.Runtime.InteropServices.WindowsRuntime;
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
            return View("Index", _hotSpotService.GetAll());
        }
        public IActionResult Create()
        {
            return View("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HotSpotModel hotspot)
        {
            var newHotSpo = _hotSpotService.AddHotSpot(hotspot);
            return View("Index");
        }
        public IActionResult Edit()
        {
            return View("Edit", _hotSpotService.GetAll());
        }
        [HttpPost]
        public IActionResult UpdateResult(int id, HotSpotModel hotSpot)
        {
            var update = _hotSpotService.Update(id,hotSpot);
            return RedirectToAction("Edit");
        }

        public IActionResult Details(int id)
        {
            return View(_hotSpotService.GetById(id));
        }
    }
}