using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using seeWifi.Interfaces;

namespace seeWifi.Controllers
{
    public class HotSpotDisplayController : Controller
    {
        private IHotSpotService _hotSpotService;
        public HotSpotDisplayController(IHotSpotService hotSpotService)
        {
            _hotSpotService = hotSpotService;
        }
        public IActionResult Index()
        {
            return View("HotSpotDisplay", _hotSpotService.GetAll());
        }
    }
}