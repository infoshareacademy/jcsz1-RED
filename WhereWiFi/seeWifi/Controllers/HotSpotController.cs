using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.AspNetCore.Mvc;
using WiFi.Library.Models;
using WiFi.Library.Services.IServices;

namespace seeWifi.Controllers
{
    public class HotSpotController : Controller
    {
        private readonly IHotSpotService _hotSpotService;
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
            return View("Create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HotSpotModel hotspot)
        {
            var newHotSpo = _hotSpotService.AddHotSpot(hotspot);
            return RedirectToAction("Index");
        }
        public IActionResult Edit()
        {
            return View("Edit", _hotSpotService.GetAll());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateResult(int id, HotSpotModel hotSpot)
        {
            var update = _hotSpotService.Update(id,hotSpot);
            return RedirectToAction("Edit");
        }
        public IActionResult Details(int id)
        {
            return View(_hotSpotService.GetById(id));
        }
        public IActionResult Favorites()
        {
            return View(_hotSpotService.GetAllFavorites());
        }
        public IActionResult MarkingHotSpotAsFavorite(int id)
        {
            _hotSpotService.MarkAsFavorite(id);
            return RedirectToAction("Index");
        }
    }
}