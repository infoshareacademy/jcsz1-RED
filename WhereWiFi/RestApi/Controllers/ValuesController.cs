﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WiFi.Library.Models.RestModels;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly string connectionString = "http://localhost:58882/";
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpGet]
        [Route("Reports")]
        public async Task<IActionResult> GetReports()
        {
            var client = HttpClientFactory.Create();
            var result = await client.GetAsync(connectionString + "api/Rest/GetAll");
            if (result.IsSuccessStatusCode == false)
            {
                return BadRequest();
            }

            var loadContent = await result.Content.ReadAsStringAsync();

            var getAllContent = JsonConvert
                .DeserializeObject<RestReportsModel[]>(loadContent);
            return Ok(getAllContent);
        }
        [HttpGet("{id}")]
        [Route("Reports/{id}")]
        public async Task<IActionResult> GetReport(int id)
        {
            var client = HttpClientFactory.Create();
            var result = await client.GetAsync(connectionString + $"api/Rest/GetAll/{id}");
            if (result.IsSuccessStatusCode == false)
            {
                return BadRequest();
            }
            var loadContent = await result.Content.ReadAsStringAsync();

            var getAllContent = JsonConvert
                .DeserializeObject<RestReportsModel>(loadContent);
            return Ok(getAllContent);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RestReportsModel restReportsModel)
        {
            var client = HttpClientFactory.Create();

            var result = await client.PostAsJsonAsync(
                new Uri(connectionString + "api/Rest/Post"),
                restReportsModel);

            if (result.IsSuccessStatusCode == false)
            {
                return BadRequest();
            }
            var loadContent = await result.Content.ReadAsStringAsync();
            var getAllContent = JsonConvert
                .DeserializeObject<RestReportsModel>(loadContent);

            return Ok(getAllContent);
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = HttpClientFactory.Create();
            var result = await client.DeleteAsync(connectionString + $"api/Rest/Delete/{id}");

            if (result.IsSuccessStatusCode == false)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpGet]
        [Route("Activity")]
        public async Task<IActionResult> Activity()
        {
            var client = HttpClientFactory.Create();
            var result = await client.GetAsync(connectionString + "api/Rest/UsersActivityReport");
            if (result.IsSuccessStatusCode == false)
            {
                return BadRequest();
            }
            var loadContent = await result.Content.ReadAsStringAsync();
            return Ok(JsonConvert.DeserializeObject(loadContent));
        }
    }
}
