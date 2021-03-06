﻿using System;
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
            var users = _adminService.GetAllUsers();
            return View(users);
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
        public IActionResult ChangeUserRole(ApplicationUserDbModel applicationUser)
        {
            _adminService.ChangeUserRole(applicationUser);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteUserExecute(int id)
        {
            if (id!=0 || id>0)
            {
                _adminService.DeleteUser(id);
            }
            return RedirectToAction("Index");
        }
        public IActionResult DeleteUser()
        {
            var users = _adminService.GetAllUsers();
            return View(users);
        }

        public IActionResult UserDetails(int id)
        {
            var user = _adminService.GetUserById(id);
            var userResult = user.Result;
            return View(userResult);
        }
    }
}