using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using seeWifi.Interfaces;
using seeWifi.Models;

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