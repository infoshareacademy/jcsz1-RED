using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using seeWifi.Models;
using WiFi.Library.DataBaseAccess.IDataBaseAccess;
using WiFi.Library.Models.RestModels;

namespace seeWifi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestController : ControllerBase
    {
        private readonly IWiFiDbContextFactory _wiFiDbContext;
        private readonly seeWifiContext _seeWifiContext;

        public RestController(IWiFiDbContextFactory wiFiDbContext, seeWifiContext seeWifiContext)
        {
            _wiFiDbContext = wiFiDbContext;
            _seeWifiContext = seeWifiContext;
        }
        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody]RestReportsModel restReports)
        {
            if (restReports == null)
            {
                return NotFound();
            }
            using (var context = _wiFiDbContext.GetDbContext())
            {
                await context.RestReports.AddAsync(restReports);
                await context.SaveChangesAsync();

                return Ok(restReports);
            }
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            using (var context = _wiFiDbContext.GetDbContext())
            {
                var listOfReports = await context.RestReports.ToListAsync();
                return Ok(listOfReports);
            }
        }
        [HttpGet("id")]
        [Route("GetAll/{id}")]
        public async Task<IActionResult> GetAllById(int id)
        {
            using (var context = _wiFiDbContext.GetDbContext())
            {
                var report = await context.RestReports.FindAsync(id);
                return Ok(report);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            using (var context = _wiFiDbContext.GetDbContext())
            {
                var singleReport = await context.RestReports.FindAsync(id);
                context.RestReports.Remove(singleReport);
                await context.SaveChangesAsync();
                return Ok();
            }
        }

        [HttpGet]
        [Route("UsersActivityReport")]
        public async Task<IActionResult> UsersActivityReport()
        {
            //logout 
            //ile adminów
            //ile jest innych userów
            //var list = _seeWifiContext.UserRoles.Select(user => user.UserId).ToList();

            var list = _seeWifiContext.Users.Select(x => x).ToList();
            return Ok(JsonConvert.SerializeObject(list));
        }
    }
}