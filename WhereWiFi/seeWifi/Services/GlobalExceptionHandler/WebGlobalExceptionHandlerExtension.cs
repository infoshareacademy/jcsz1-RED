using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace seeWifi.Services.GlobalExceptionHandler
{
    public static class WebGlobalExceptionHandlerExtension
     {
        public static IApplicationBuilder UseAspNetCoreExceptionHandler(this IApplicationBuilder app)
        {
            var loggerFactory = app.ApplicationServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;

            return app.UseExceptionHandler(HandleException(loggerFactory));
        }

        public static Action<IApplicationBuilder> HandleException(ILoggerFactory loggerFactory)
        {
            return appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerFeature != null)
                    {
                        var logger = loggerFactory.CreateLogger("Serilog global exception logger");
                        logger.LogError(500, exceptionHandlerFeature.Error, exceptionHandlerFeature.Error.Message);
                    }

                    context.Response.Redirect($"/Error/Status/{context.Response.StatusCode}");
                });
            };
        }
    }
}
