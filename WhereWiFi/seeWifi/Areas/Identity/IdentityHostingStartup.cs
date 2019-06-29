using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using seeWifi.Areas.Identity.Data;
using seeWifi.Models;

[assembly: HostingStartup(typeof(seeWifi.Areas.Identity.IdentityHostingStartup))]
namespace seeWifi.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<seeWifiContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("seeWifiContextConnection")));

                services.AddDefaultIdentity<User>()
                    .AddEntityFrameworkStores<seeWifiContext>();
            });
        }
    }
}