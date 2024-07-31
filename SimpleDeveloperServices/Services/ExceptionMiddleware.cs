using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleDeveloperCore.IServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace SimpleDeveloperServices.Services
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        //public async Task InvokeAsync(HttpContext httpContext, IWebHostEnvironment env, ExceptionLogging logger)
        //{
        //    try
        //    {
        //        await _next(httpContext);
        //    }
        //    catch (Exception error)
        //    {
        //        var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
        //        var ex = exceptionDetails?.Error ?? error;

        //        // Should always exist, but best to be safe!
        //        if (ex != null)
        //        {
        //            var response = await logger.SendErrorToMailAsync(ex);
        //            // ProblemDetails has it's own content type
        //            httpContext.Response.ContentType = "application/problem+json";

        //            var includeDetails = env.IsDevelopment();
        //            // Get the details to display, depending on whether we want to expose the raw exception
        //            var title = includeDetails ? "An error occured: " + ex.Message : "An error occured";
        //            var details = includeDetails ? ex.ToString() : null;

        //            var problem = new ProblemDetails
        //            {
        //                Status = 500,
        //                Title = title,
        //                Detail = details
        //            };

        //            // This is often very handy information for tracing the specific request
        //            var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        //            if (traceId != null)
        //            {
        //                problem.Extensions["traceId"] = traceId;
        //            }

        //            //Serialize the problem details object to the Response as JSON (using System.Text.Json)
        //            var stream = httpContext.Response.Body;
        //            await JsonSerializer.SerializeAsync(stream, problem);
        //        }
        //        //var response = await logger.SendErrorToMailAsync(ex);

        //        //var problem = new ProblemDetails()
        //        //{
        //        //    Status = 500,
        //        //    Title = ex.Message,
        //        //    Detail = ex.StackTrace,
        //        //};
        //        //if (problem != null)
        //        //    await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(problem));
        //    }
        //}
    }
}
