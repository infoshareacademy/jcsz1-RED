using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using seeWifi.Services.GlobalExceptionHandler;
using WiFi.Library.DataBaseAccess;
using WiFi.Library.DataBaseAccess.IDataBaseAccess;
using WiFi.Library.Services;
using WiFi.Library.Services.IServices;


namespace seeWifi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(o =>
            {
                // We will put our translations in a folder called Resources
                o.ResourcesPath = "Resources";
            });
            services.AddMvc();
            services.AddMvc()
                .AddViewLocalization(
                    LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("pl-PL"),
                    };

                    opts.DefaultRequestCulture = new RequestCulture("en-US");
                    opts.SupportedCultures = supportedCultures;
                    opts.SupportedUICultures = supportedCultures;
                });
            services.AddSingleton<IWiFiDbContextFactory, WiFiDbContextFactory>();
            services.AddSingleton<IAdminService, AdminService>();
            services.AddSingleton<IHotSpotService, HotSpotService>();
            services.AddSingleton<IReportsService, ReportsService>();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAutoMapper(Assembly.GetAssembly(typeof(Startup)));
            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("pl-PL"),
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseAspNetCoreExceptionHandler();
                //app.UseExceptionHandler("/Home/Error");
                //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            app.UseStatusCodePagesWithRedirects("/Error/Status/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUi3();
        }
    }
}
