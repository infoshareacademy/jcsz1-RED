using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WiFi.Library.Models.ModelsForDB;
using WiFi.Library.Services.IServices;

namespace seeWifi.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EditUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(ApplicationUserDbModel applicationUser)
        {
            var user = _adminService.CreateUser(applicationUser);
            return RedirectToAction("Index");
        }
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangeUserRole(int id,int userRole)
        {
            _adminService.ChangeUserRole(id, userRole);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            if (id!=0 || id>0)
            {
                _adminService.DeleteUser(id);
            }
            return RedirectToAction("Index");
        }
    }
}