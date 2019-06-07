﻿using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using seeWifi.ViewModels;
using WiFi.Library.Models;
using WiFi.Library.Services.IServices;

namespace seeWifi.Controllers
{
    public class HotSpotController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IHotSpotService _hotSpotService;

        public HotSpotController(IMapper mapper, IHotSpotService hotSpotService)
        {
            _mapper = mapper;
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
        public IActionResult Create(HotSpotViewModel hotspot)
        {
            var newHotSpo = _hotSpotService.AddHotSpot(_mapper.Map< HotSpotViewModel, HotSpotModel>(hotspot));
            return RedirectToAction("Index");
        }

        public IActionResult Edit()
        {
            var modelHotSpotList = _hotSpotService.GetAll().Select(x=> _mapper.Map<HotSpotModel, HotSpotViewModel>(x)).ToList();
            return View("Edit", modelHotSpotList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateResult(int id, HotSpotViewModel hotSpotView)
        {
            var update = _hotSpotService.Update(id, _mapper.Map<HotSpotViewModel, HotSpotModel>(hotSpotView));
            return RedirectToAction("Edit");
        }

        public IActionResult Details(int id)
        {
            var editedHotSpot =_mapper.Map<HotSpotModel, HotSpotViewModel>(_hotSpotService.GetById(id));
            return View(editedHotSpot);
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

        public IActionResult DeleteFavorite(int id)
        {
            _hotSpotService.MarkAsFavorite(id);
            return RedirectToAction("Favorites");
        }

        public IActionResult Nearest(HotSpotModel hotspot)
        {
            return View("Nearest", _mapper.Map<HotSpotModel, HotSpotViewModel>(hotspot));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CompareNearest(HotSpotViewModel hotspot)
        {
            var nearestHotSpot = _hotSpotService.NearestHotSpot(_mapper.Map<HotSpotViewModel, HotSpotModel>(hotspot));
            return View("ComparedHotSpots", nearestHotSpot);
        }
    
    }
}