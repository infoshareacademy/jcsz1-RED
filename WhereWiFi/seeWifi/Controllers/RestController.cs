using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WiFi.Library.DataBaseAccess.IDataBaseAccess;
using WiFi.Library.Models.RestModels;

namespace seeWifi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestController : ControllerBase
    {
        private readonly IWiFiDbContextFactory _wiFiDbContext;

        public RestController(IWiFiDbContextFactory wiFiDbContext)
        {
            _wiFiDbContext = wiFiDbContext;
        }
        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody]RestReportsModel restReports)
        {
            if (restReports ==null)
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
            using (var context =_wiFiDbContext.GetDbContext())
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
        [HttpGet]
        [Route("UsersActivityReport")]
        public async Task<IActionResult> UsersActivityReport()
        {
            using (var context = _wiFiDbContext.GetDbContext())
            {
                var usersList =await context.ApplicationUser.ToListAsync();
                return Ok(JsonConvert.SerializeObject(usersList));
            }

        }
    }
}